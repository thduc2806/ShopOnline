using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.ViewModels.Account
{
	public class RegisterVm
	{
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
		public string FullName { get; set; }
		public int Phone { get; set; }

		public string ReturnUrl { get; set; }
    }
}
