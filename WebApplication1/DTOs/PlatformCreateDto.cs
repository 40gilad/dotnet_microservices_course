﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
	public class PlatformCreateDto
	{

		[Required]
		public string Name { get; set; }

		[Required]
		public string Publisher { get; set; }

		[Required]
		public int Cost { get; set; }
	}
}
