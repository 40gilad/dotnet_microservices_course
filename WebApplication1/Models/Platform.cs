﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Platform
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

		[Required]
		public string Publisher { get; set; }

		[Required]
		public int Cost { get; set; }   
    }
}
