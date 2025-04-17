using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{

	[Route("api/commands/[controller]")]
	[ApiController]
	public class PlatformController: ControllerBase
	{
        public PlatformController()
        {
            
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> CommandService.Inbound");
            return Ok("Inbound test from CommandService.PlatformController");
        }

    }
}
