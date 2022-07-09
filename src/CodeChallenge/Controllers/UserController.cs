using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Commands.Base;
using CodeChallenge.Services.DataAccess.Queries.Base;
using CodeChallenge.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IValidator<Register> _registerValidator;
        private readonly IValidator<Login> _loginValidator;
        private readonly IJWTservice _jwtService;
        private readonly ISender _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(ISender mediator , IValidator<Register> registerValidator, IValidator<Login> loginValidator , IJWTservice jwtService, ILogger<UserController> logger)
        {
            _logger = logger;
            _mediator = mediator;
            _jwtService = jwtService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]")]
        public async Task<ActionResult<Response>> Signup([FromBody] Register register)
        {
            _logger.LogDebug($"Signup request received :  {System.Text.Json.JsonSerializer.Serialize(register)}");
            var validate = await _registerValidator.ValidateAsync(register);
            if (!validate.IsValid)
            {
                _logger.LogDebug($"Invalid request received :  {System.Text.Json.JsonSerializer.Serialize(validate.Errors)}");
                return BadRequest(new Response
                {
                    Status = ResponseStatus.Error,
                    Message = validate.Errors.Select(error => (error.ErrorMessage)).Tojson()
                });
            }
            var userExists = await  _mediator.Send(new CheckUserExists ( register.Username! ));
            if (!userExists)
                return (await _mediator.Send(new AddNewUser(register)))
                    ? new Response() { Status = ResponseStatus.Success, Message = "OK" }
                    : new Response() { Status = ResponseStatus.Error, Message = "Error" };
            _logger.LogDebug($"User already exists :  {System.Text.Json.JsonSerializer.Serialize(register)}");
            return Conflict(new Response
            {
                Status = ResponseStatus.Error,
                Message = "Username already exists"
            });
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async  Task<ActionResult<Response>> Login([FromBody] Login login)
        {
            _logger.LogDebug("Login request received :  " + System.Text.Json.JsonSerializer.Serialize(login));
            var validate = await _loginValidator.ValidateAsync(login);
            if (!validate.IsValid)
            {
                _logger.LogDebug("Invalid request received :  " + System.Text.Json.JsonSerializer.Serialize(validate.Errors));
                return  BadRequest(new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  validate.Errors.Select(error => (error.ErrorMessage)).Tojson() 
                }) ;
            }
            var userExists = await  _mediator.Send(new CheckUserExists ( login.Username! ));
            if (!userExists)
            {
                _logger.LogDebug("User does not exist :  " + System.Text.Json.JsonSerializer.Serialize(login));
                return BadRequest(new Response
                {
                    Status = ResponseStatus.Error,
                    Message = "Username does not exist"
                });
            }
            var userPasswordCheck = await _mediator.Send(new CheckUserPassword(login.Username!, login.Password!));
            if (!userPasswordCheck)
            {
                _logger.LogDebug("Invalid password :  " + System.Text.Json.JsonSerializer.Serialize(login));
                return  Unauthorized(new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  "Password is incorrect"
                }) ;
            }
            var token = await _jwtService.GetToken(new List<Claim> {new(ClaimTypes.Name, login.Username!)});
            return new Response
            {
                Status = ResponseStatus.Success,
                Message =   new JwtSecurityTokenHandler().WriteToken(token),
            };
        }

    }
}
