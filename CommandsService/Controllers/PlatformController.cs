using AutoMapper;
using CommandsService.Data;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{

	[Route("api/commands/[controller]")]
	[ApiController]
	public class PlatformController: ControllerBase
	{
		private readonly ICommandRepo _repo;
		private readonly IMapper _mapper;

		public PlatformController(ICommandRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> CommandService.Inbound");
            return Ok("Inbound test from CommandService.PlatformController");
        }

    }
}
