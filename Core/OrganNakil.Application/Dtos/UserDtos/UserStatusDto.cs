using System;
namespace OrganNakil.Application.Dtos.UserDtos
{
	public class UserStatusDto
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public Guid? UserId { get; set; }
	}
}

