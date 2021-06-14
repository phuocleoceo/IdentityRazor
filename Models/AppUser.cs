using Microsoft.AspNetCore.Identity;
namespace IdentityRazor.Models
{
	public class AppUser : IdentityUser
	{
		public string University { get; set; }
	}
}