using WebApplication1.DTOs;

namespace WebApplication1.SyncDataServices.Http
{
	public interface ICommandDataClient
	{
		Task SendPatformCommand(PlatformReadDto p);
	}
}
