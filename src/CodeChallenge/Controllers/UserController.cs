using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Identity;
using CodeChallenge.Models.Interfaces;
using CodeChallenge.Models.Types;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ISender _mediator;

        public UserController(ISender mediator , IConfiguration config, IValidator<Register> registerValidator, IValidator<Login> loginValidator , IJWTservice jwtService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _mediator = mediator;
            _jwtService = jwtService;
            _config = config;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _userManager = userManager;
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
            var userExists = await  _mediator.Send(new CheckUserExists { UserName = register.Username! });
            if (userExists)
            {
                return new Response
                {
                    Status = ResponseStatus.Error,
                    Message =  "Username already exists"
                };
            }
            return Ok(userExists);
        }

    }
}
