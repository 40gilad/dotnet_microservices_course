using CommandsService.Models;
using System;

namespace CommandsService.Data
{
	public class CommandRepo : ICommandRepo
	{
		private readonly AppDbContext _context;

		public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformId, Command command)
		{
			if (command == null)
				throw new ArgumentNullException(nameof(command));

			//if (!PlatformExist(platformId))
			//	throw new InvalidOperationException($"platformId {platformId} does not exist!");
			
			command.PlatformId= platformId;
			_context.Commands.Add(command);
		}

		public void CreatePlatform(Platform plat)
		{
			if(null == plat)
				throw new ArgumentNullException(nameof(plat));
			_context.Platforms.Add(plat);
		}

		bool ExternalPlatformExists(int externalPlatformId)
		{
			return (_context.Platforms.Any(p => p.ExternalId == externalPlatformId));
		}

		public IEnumerable<Platform> GetAllPlatforms()
		{
			return _context.Platforms.ToList();
		}

		public Command? GetCommand(int platformId, int commandId)
		{
			return _context.Commands
				.Where(c => c.PlatformId == platformId && c.Id == commandId)
				.FirstOrDefault();
		}

		public IEnumerable<Command> GetCommandsForPlatform(int platformId)
		{
			return _context.Commands
				.Where(c => c.PlatformId == platformId)
				.OrderBy(c => c.Platform.Name);
		}

		public bool PlatformExist(int platformId)
		{
			return (_context.Platforms.Any(p=>p.Id == platformId));
		}

		public bool SaveChanges()
		{
			return (_context.SaveChanges()>=0);
		}
	}
}
