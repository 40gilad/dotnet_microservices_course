using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{

	[Route("api/commands/[controller]")]
	[ApiController]
	public class PlatformsController: ControllerBase
	{
		private readonly ICommandRepo _repository;
		private readonly IMapper _mapper;

		public PlatformsController(ICommandRepo repository,IMapper mapper)
        {
			_repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from CommandService");
            var platforms= _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> CommandService.Inbound");
            return Ok("Inbound test from CommandService.PlatformController");
        }

    }
}
