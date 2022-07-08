using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Commands.Base;
using CodeChallenge.Services.DataAccess.Queries.Base;
using CodeChallenge.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IValidator<Register> _registerValidator;
        private readonly IValidator<Login> _loginValidator;
        private readonly IJWTservice _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ISender _mediator;

        public UserController(ISender mediator , IConfiguration config, IValidator<Register> registerValidator, IValidator<Login> loginValidator , IJWTservice jwtService,
            RoleManager<IdentityRole> roleManager)
        {
            _mediator = mediator;
            _jwtService = jwtService;
            _config = config;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]")]
        public async Task<ActionResult<Response>> Signup([FromBody] Register register)
        {
            var validate = await _registerValidator.ValidateAsync(register);
            if (!validate.IsValid)
            {
                return new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  validate.Errors.Select(error => (error.ErrorMessage)).Tojson() 
                };
            }
            var userExists = await  _mediator.Send(new CheckUserExists ( register.Username! ));
            if (userExists)
            {
                return new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  "Username already exists"
                };
            }
            return  (await _mediator.Send(new AddNewUser(register))) ? new Response() {Status = ResponseStatus.Success , Message = "OK"} : new Response(){Status = ResponseStatus.Error , Message = "Error"};
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async  Task<ActionResult<Response>> Login([FromBody] Login login)
        {
            var validate = await _loginValidator.ValidateAsync(login);
            if (!validate.IsValid)
            {
                return new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  validate.Errors.Select(error => (error.ErrorMessage)).Tojson() 
                };
            }
            var userExists = await  _mediator.Send(new CheckUserExists ( login.Username! ));
            if (!userExists)
            {
                return new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  "Username does not exist"
                };
            }
            var userPasswordCheck = await _mediator.Send(new CheckUserPassword(login.Username!, login.Password!));
            if (!userPasswordCheck)
            {
                return new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  "Password is incorrect"
                };
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
