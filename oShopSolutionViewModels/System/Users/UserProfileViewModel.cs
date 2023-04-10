using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
    public class UserProfileViewModel
    {
        public Guid UserId { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? FullName { get; set; }

		public DateTime? DOB { get; set; }

		public string? Street { get; set; }

		public string? City { get; set; }

		public string? State { get; set; }

		public string? Ward { get; set; }

		public string? Country { get; set; }

		public string? PhoneNumber { get; set; }

		public bool IsActivated { get; set; }

		public Guid? CreatedBy { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? TokenEffectiveDate { get; set; }

		public long? TokenEffectiveTimeStick { get; set; }

		public string? RefreshToken { get; set; }

		public List<UserRoleViewModel> UserRoles { get; set; }
    }
}
