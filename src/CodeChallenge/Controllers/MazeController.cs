
using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Types;
using CodeChallenge.Services.DataAccess.Commands.Base;
using CodeChallenge.Services.DataAccess.Queries.Base;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    public class MazeController : ControllerBase
    {
        private readonly IValidator<MazeDTO> _mazeValidator;
        private readonly ISender _mediator;
        public MazeController(IValidator<MazeDTO> mazeValidator,ISender mediator)
        {
            _mazeValidator = mazeValidator;
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        [Route("/[controller]")]
        public async Task<ActionResult<Response>> AddMaze([FromBody] MazeDTO maze)
        {
            var mazeValid = await _mazeValidator.ValidateAsync(maze);
            if (!mazeValid.IsValid)
            {
                return BadRequest(mazeValid.Errors);
            }
            var resultMazeHash = await  _mediator.Send(new AddNewMaze(maze , User) );
            return Ok(new Response()
            {
                Status = ResponseStatus.Success,
                Message = resultMazeHash
            });
        }
        [Authorize]
        [HttpGet]
        [Route("/[controller]/{mazeHash}/image/first_stage")]
        public async Task<ActionResult<Response>> GetMazeImage(string mazeHash)
        {
            var resultMaze = await _mediator.Send(new GetMaze(mazeHash, User));
            if (resultMaze == null)
            {
                return NotFound(new Response
                {
                    Status = ResponseStatus.Error,
                    Message = "Maze not found"
                });
            }
            return File(resultMaze.FirstStageImage!, "image/png");
        }
        [Authorize]
        [HttpGet]
        [Route("/[controller]")]
        public async Task<ActionResult<Response>> GetMaze(string mazeHash)
        {
            var resultMaze = await _mediator.Send(new GetMaze(mazeHash, User));
            if (resultMaze == null)
            {
                return NotFound(new Response
                {
                    Status = ResponseStatus.Error,
                    Message = "You don't have such a maze"
                });
            }
            return Ok(new Response()
            {
                Status = ResponseStatus.Success,
                Message = resultMaze.StringRepresentation
            });
        }

    }
}