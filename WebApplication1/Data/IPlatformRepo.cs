using WebApplication1.Models;

namespace WebApplication1.Data
{
	public interface IPlatformRepo
	{
		bool SaveChanges();

		IEnumerable<Platform> GetAll();
		Platform GetPlatformById(int id);
		void CreatePlatform(Platform p);
	}
}
