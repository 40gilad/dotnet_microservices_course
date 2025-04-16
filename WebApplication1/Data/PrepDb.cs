using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApplication1.Models;
namespace WebApplication1.Data
{
	public static class PrepDb
	{
		public static void PrepPopulation(IApplicationBuilder app)
		{
			using (var serviceScoped = app.ApplicationServices.CreateScope())
			{
				SeeedData(serviceScoped.ServiceProvider.GetService<AppDbContext>());
			}
		}
		private static void SeeedData(AppDbContext context)
		{
			Console.WriteLine("PrepDb.SeeedData -->\n");
			if (!context.Platforms.Any())
			{
				Console.WriteLine("\tSeeding Data...");

				context.Platforms.AddRange(
					new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = 0 },
					new Platform { Name = "SQL Server Express", Publisher = "Microsoft", Cost = 0 },
					new Platform { Name = "Kubernetes", Publisher = "Cloud Native", Cost = 0 }
				);

				context.SaveChanges();
			}
			else
				Console.WriteLine("\tWe already have data");
		}
	}
}
