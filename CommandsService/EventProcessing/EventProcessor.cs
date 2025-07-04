﻿using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using CommandsService.Models;
using System.Text.Json;

namespace CommandsService.EventProcessing
{

	public class EventProcessor : IEventProcessor
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly IMapper _mapper;

		public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
		{
			_scopeFactory = scopeFactory;
			_mapper = mapper;
		}

		public void ProcessEvent(string msg)
		{
			var eventType = DetermineEvent(msg);

			switch (eventType)
			{
				case EventType.PlatformPublished:
					addPlatform(msg);
					break;

				default:
					break;
			}
		}

		private EventType DetermineEvent(string msg)
		{
			Console.WriteLine($"--> Determing Event");
			var eventType = JsonSerializer.Deserialize<GenericEventDto>(msg);

			if (eventType == null)
				return EventType.Undetermined;
			switch (eventType.Event)
			{
				case "Platform_Published":
					Console.WriteLine($"--> Platform publish event detected");
					return EventType.PlatformPublished;
				default:
					Console.WriteLine($"--> Couldnt Determine event type");
					return EventType.Undetermined;
			}
		}

		private void addPlatform(string platformPublishedMessage)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
				var platformPublishedDto = JsonSerializer.
					Deserialize<PlatformPublishedDto>(platformPublishedMessage);

				try
				{
					var plat = _mapper.Map<Platform>(platformPublishedDto);
					if (!repo.ExternalPlatformExists(plat.Id))
					{
						repo.CreatePlatform(plat);
						repo.SaveChanges();
						Console.WriteLine($"--> Platform {plat.Id} was added");

					}
					else

						Console.WriteLine("--> Platform already exist!");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Could not add Platform to DB: {ex.Message}");
				}
			}
		}
	}
	enum EventType
	{
		PlatformPublished,
		Undetermined
	}
}
