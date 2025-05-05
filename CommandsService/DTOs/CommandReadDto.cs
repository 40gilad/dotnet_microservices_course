using CommandsService.Models;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.DTOs
{
	public class CommandReadDto
	{
		public int Id { get; set; }
		required public string HowTo { get; set; }
		required public string CommandLine { get; set; }
		public int PlatformId { get; set; }
	}
}
