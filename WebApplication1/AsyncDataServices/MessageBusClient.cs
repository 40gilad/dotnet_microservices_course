using WebApplication1.DTOs;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace WebApplication1.AsyncDataServices
{
	public class MessageBusClient : IMessageBusClient
	{
		private readonly IConfiguration _configuration;
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public MessageBusClient(IConfiguration configuration)
        {
            _configuration= configuration;
			var factory = new ConnectionFactory()
			{
				HostName = _configuration["RabbitMQHost"],
				Port = int.Parse(_configuration["RabbitMQPort"])
			};

			try
			{
				_connection = factory.CreateConnection();
				_channel = _connection.CreateModel();

				_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

				_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

				Console.WriteLine("--> Connected to MessageBus");
			}
			catch (Exception e) 
			{
				Console.WriteLine($"Could not connect to the Message Bus: {e.ToString()}");
			}
        }
		public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
		{
			var message = JsonSerializer.Serialize(platformPublishedDto);
			if (_connection.IsOpen)
			{
				Console.WriteLine($"--> RabbitMQ Connection Open, sending message...");
				SendMessage(message);
			}
			else
			{
				Console.WriteLine($"--> RabbitMQ Connection Closed, Not sending message");
			}
		}

		private void SendMessage(string msg)
		{
			var body = Encoding.UTF8.GetBytes(msg);
			_channel.BasicPublish(
				exchange: "trigger",
				routingKey: "",
				basicProperties: null,
				body: body
				);
			Console.WriteLine($"--> Message was sent: {msg}");
		}

		public void Dispose()
		{
			Console.WriteLine("MessageBus Disposed");
			if (_channel.IsOpen)
			{
				_channel.Close();
				_connection.Close();
			}
		}
		private void RabbitMQ_ConnectionShutdown(object? sender,ShutdownEventArgs e)
		{
			Console.WriteLine($"--> RabbitMQ Connection Shutdown");
		}
	}
}
