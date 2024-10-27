using System;
using Microsoft.AspNetCore.Identity;


namespace OrganNakil.Domain.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string BloodGroup { get; set; }
	}
}

