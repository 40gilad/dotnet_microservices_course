﻿
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CommandsService.AsyncDataServices
{
	public class MessageBusSubscriber : BackgroundService
	{
		private readonly IConfiguration _configuration;
		private readonly IEventProcessor _eventProcessor;
		private IConnection _connection;
		private IModel _channel;
		private string _queueName;

		public MessageBusSubscriber(IConfiguration  configuration, IEventProcessor eventProcessor)
        {
			_configuration = configuration;
			_eventProcessor = eventProcessor;
			InitializeRabbitMQ();
		}

		private void InitializeRabbitMQ()
		{
			var factory = new ConnectionFactory()
			{
				HostName = _configuration["RabbitMQHost"],
				Port = int.Parse(_configuration["RabbitMQPort"])
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
			_queueName = _channel.QueueDeclare().QueueName;
			_channel.QueueBind(
				queue: _queueName,
				exchange : "trigger",
				routingKey: ""
				);
			Console.WriteLine("--> Listening on the message Bus");
			_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
		}

		private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
		{
			Console.WriteLine("--> Connection Shutdown");
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();

			var consumer = new EventingBasicConsumer(_channel);

			consumer.Received += (ModuleHandle, ea) =>
			{
				Console.WriteLine("--> Event Recived!");
				var body = ea.Body;
				var notifiactionMessage = Encoding.UTF8.GetString(body.ToArray());

				_eventProcessor.ProcessEvent(notifiactionMessage);
			};

			_channel.BasicConsume(
				queue: _queueName,
				autoAck: true,
				consumer: consumer
				);
			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			if (_channel.IsOpen)
			{
				_channel.Close();
				_connection.Close();
			}
			base.Dispose();
		}
	}
}
