using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Data

{
	public static class PrepDb
	{
		public static void PrepPopulation(IApplicationBuilder app,bool isProd)
		{
			using (var serviceScoped = app.ApplicationServices.CreateScope())
			{
				SeeedData(serviceScoped.ServiceProvider.GetService<AppDbContext>(),isProd);
			}
		}
		private static void SeeedData(AppDbContext context,bool is_prod)
		{
			if(is_prod)
			{
				Console.WriteLine("--> Trying to apply migraions...");
				try
				{
					context.Database.Migrate();
				}
				catch(Exception ex)
				{
					Console.WriteLine($"\t--> Could not run migrations: {ex.Message}");
				}
			}
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
