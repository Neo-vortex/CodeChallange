
using CodeChallenge.Models;
using CodeChallenge.Models.DTOs;
using CodeChallenge.Models.Enums;
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
    public class MazeController : ControllerBase
    {
        private readonly IValidator<MazeDTO> _mazeValidator;
        private readonly ISender _mediator;
        private readonly IMazeService _mazeService;
        public MazeController(IValidator<MazeDTO> mazeValidator,ISender mediator, IMazeService mazeService)
        {
            _mazeValidator = mazeValidator;
            _mediator = mediator;
            _mazeService = mazeService;
        }
        
        [Authorize]
        [HttpGet]
        [Route("/[Controller]/{mazeHash}/solution")]
        public async Task<ActionResult<SolutionResponse>> GetSolution(string mazeHash, string steps)
        {
            Solution solution;
            try
            {
                solution = new Solution(steps);
            }
            catch (Exception)
            {
                return BadRequest(new Response()
                {
                    Status = ResponseStatus.Error,
                    Message = $"solution type is unknown : {steps}"
                });
            }
            var resultMaze = await _mediator.Send(new GetMaze(mazeHash, User));
            if (resultMaze == null)
            {
                return NotFound(new Response
                {
                    Status = ResponseStatus.Error,
                    Message = "You don't have such a maze"
                });
            }
            if (!resultMaze.PrimitiveAnalysis)
            {
                 await  _mediator.Send(new UpdatePrimitiveAnalysis(await _mazeService.ApplyPrimitiveAnalysis(resultMaze)) );
            }
            switch (solution.Type)
            {
                case SolutionType.MIN: 
                     var result = await _mazeService.SolveMinPath(resultMaze);
                     await _mediator.Send(new UpdateMazeMinPathSolution(result));
                     return (new SolutionResponse()
                     {
                         path = result.ShortestPath!.Select(path => path.ToPlainText()).ToArray()
                     });
                case SolutionType.MAX:
                    break;
            }
            
            return Ok();

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
        [Route("/[controller]/{mazeId}/image/first_stage")]
        public async Task<ActionResult<Response>> GetMazeImage(string mazeId)
        {
            var resultMaze = await _mediator.Send(new GetMaze(mazeId, User));
            if (resultMaze == null)
            {
                return NotFound(new Response
                {
                    Status = ResponseStatus.Error,
                    Message = "Maze not found"
                });
            }
            return File(resultMaze.Image!, "image/png");
        }
        [Authorize]
        [HttpGet]
        [Route("/[controller]")]
        public async Task<ActionResult<Response>> GetMaze(string? mazeHash = null)
        {
            if (mazeHash == null)
            {
                var resultMazes = (await _mediator.Send(new GetAllMazes( User))).Select(maze =>  System.Text.Json.JsonSerializer.Deserialize<MazeDTO>(maze.StringRepresentation)! with { Hash = maze.Hash}  ).ToList();

                return Ok(new Response()
                {
                    Status = ResponseStatus.Success,
                    Message = resultMazes.Tojson()
                });

            }
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