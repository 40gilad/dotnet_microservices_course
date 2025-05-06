using WebApplication1.DTOs;

namespace WebApplication1.AsyncDataServices
{
	public interface IMessageBusClient
	{
		void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
	}
}
