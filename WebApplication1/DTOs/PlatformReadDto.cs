using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
	public class PlatformReadDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Publisher { get; set; }
		public int Cost { get; set; }
	}
}
