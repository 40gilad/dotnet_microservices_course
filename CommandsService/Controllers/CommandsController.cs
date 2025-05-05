using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CommandsService.Controllers
{
	[Route("api/commands/platforms/{platformId}/[controller]")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly ICommandRepo _repository;
		private readonly IMapper _mapper;

		public CommandsController(ICommandRepo repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CommandReadDto>> GetAll(int platformId)
		{
			Console.WriteLine($"--> Getting All Commands for Platform {platformId}");
			if(!_repository.PlatformExist(platformId))
				return NotFound();

			var commands = _repository.GetCommandsForPlatform(platformId);
			return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
		}


		[HttpGet("{commandId}", Name = "GetCommandForPlatform")]
		public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
		{
			Console.WriteLine($"--> Getting Platform's {platformId} Command {commandId}");
			if (!_repository.PlatformExist(platformId))
				return NotFound();

			var command = _repository.GetCommand(platformId,commandId);
			if (command == null)
				return NotFound($"Command id {commandId} dosent exist!");
			return Ok(_mapper.Map<CommandReadDto>(command));
		}

		[HttpPost]
		public ActionResult CreateCommand(int platformId, [FromBody] CommandCreateDto c)
		{
			if (!_repository.PlatformExist(platformId))
				return NotFound();
			if (!ModelState.IsValid)
				return BadRequest($"{nameof(c)}");

			Console.WriteLine("--> Creating command");
			_repository.CreateCommand(platformId,_mapper.Map<Command>(c));
			_repository.SaveChanges();

			var commandReadDto = _mapper.Map<CommandReadDto>(c);
			return CreatedAtRoute(nameof(GetCommandForPlatform),
				new
				{
					platformId = platformId,
					commandId = commandReadDto.Id
				},
				commandReadDto);
		}
	}
}
