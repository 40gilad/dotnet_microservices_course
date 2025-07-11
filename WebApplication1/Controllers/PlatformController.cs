﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.AsyncDataServices;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.SyncDataServices.Http;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class PlatformController : ControllerBase
	{
        private readonly IMapper _mapper;
        private readonly IPlatformRepo _repository;
		private readonly ICommandDataClient _commandDataClient;
		private readonly IMessageBusClient _messageBusClient;

		public PlatformController(
            IPlatformRepo repository,
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
			_mapper = mapper;
            _repository = repository;
            _commandDataClient = commandDataClient;
			_messageBusClient = messageBusClient;
		}
            
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting All Platforms...");
            var platforms = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name= "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
			Console.WriteLine($"--> Getting Platforms with id {id}");
            var platform = _repository.GetPlatformById(id);
            if (platform == null)
                return NotFound();
            return Ok(_mapper.Map<PlatformReadDto>(platform));
		}

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto p)
        {
            Console.WriteLine("--> Create Platform...");
            var platform = _mapper.Map<Platform>(p);
            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var ret_platform = _mapper.Map<PlatformReadDto>(platform);

            // Send Sync Message
            try
            {
                Console.WriteLine(_commandDataClient);
                await _commandDataClient.SendPatformCommand(ret_platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreatePlatform:\t--> Could not send synchrunisly {ex.Message}");
            }

			// Send Async Message
			try
			{
				var publish_platform = _mapper.Map<PlatformPublishedDto>(ret_platform);
                publish_platform.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(publish_platform);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"CreatePlatform:\t--> Could not send Asynchrunisly {ex.Message}");
			}

			return CreatedAtRoute(nameof(GetPlatformById),new {Id= ret_platform.Id}, ret_platform);
		}
	}
}
