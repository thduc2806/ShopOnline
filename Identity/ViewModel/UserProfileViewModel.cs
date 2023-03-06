namespace Identity.ViewModel
{
    public class UserProfileViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActivated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public DateTime? DOB { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool HasPassWord { get; set; }

        public List<UserRolesViewModel> UserRoles { get; set; }
    }
}
