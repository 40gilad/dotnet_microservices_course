﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
	public class PlatformRepo : IPlatformRepo
	{

		private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform p)
		{
			if (p == null)
				throw new ArgumentNullException(nameof(p));
			_context.Platforms.Add(p);
		}

		public IEnumerable<Platform> GetAll()
		{
			return _context.Platforms.ToList();
		}

		public Platform GetPlatformById(int id)
		{
			return _context.Platforms.FirstOrDefault(p => p.Id == id);
		}

		public bool SaveChanges()
		{
			return (_context.SaveChanges() >= 0);
		}
	}
}
