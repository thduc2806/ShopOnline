using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModel
{
    public class RegisterModel
    {
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password length at least 8 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }

        public string RoleId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? MerchantFid { get; set; }
        [Required]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid Phone number")]
        [MinLength(length: 8, ErrorMessage = "The Phone Number minimum length of 8 characters")]
        public string PhoneNumber { get; set; }
        public string DomainFid { get; set; }
        [Required]
        public string CountryPhone { get; set; }

        public string PhoneCode => CountryPhone;
        public string FacebookId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
