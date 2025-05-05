using CommandsService.Models;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.DTOs
{
	public class CommandCreateDto
	{
		[Required]
		required public string HowTo { get; set; }
		[Required]
		required public string CommandLine { get; set; }
	}
}
