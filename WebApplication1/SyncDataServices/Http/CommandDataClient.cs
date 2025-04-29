using System.Net.Http;
using System.Text;
using System.Text.Json;
using WebApplication1.DTOs;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.SyncDataServices.Http
{
	public class CommandDataClient : ICommandDataClient
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public CommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
			_httpClient = httpClient;
			_configuration = configuration;

		}

        public async Task SendPatformCommand(PlatformReadDto p)
		{
			var httpContent = new StringContent(
				JsonSerializer.Serialize(p),
				Encoding.UTF8,
				"application/json"
			);
			Console.WriteLine($"sending to {_configuration["CommandService"]}");
			var response = await _httpClient.PostAsync(
				_configuration["CommandService"],
				httpContent
			);
			Console.WriteLine("SendPatformCommand:\n\t");

			if (response.IsSuccessStatusCode)
				Console.WriteLine("--> Sync POST to CommandService was OK");
			else
				Console.WriteLine("--> Sync POST to CommandService was NOT OK");

		}
	}
}
