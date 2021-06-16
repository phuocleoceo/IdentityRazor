using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
namespace IdentityRazor.Models
{
	public class AppUser : IdentityUser
	{
		[MaxLength(100)]
		public string FullName { get; set; }

		[MaxLength(255)]
		public string Address { get; set; }

		[MaxLength(255)]
		public string University { get; set; }

		[DataType(DataType.Date)]
		public DateTime? Birthday { get; set; }
	}
}