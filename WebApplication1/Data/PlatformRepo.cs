using Microsoft.EntityFrameworkCore;
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
			throw new NotImplementedException();
		}

		public IEnumerable<Platform> GetAll()
		{
			throw new NotImplementedException();
		}

		public Platform GetPlatformById(int id)
		{
			throw new NotImplementedException();
		}

		public bool SaveChanges()
		{
			return (_context.SaveChanges() >= 0);
		}
	}
}
