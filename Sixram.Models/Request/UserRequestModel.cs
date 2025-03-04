namespace Sixram.Models.Request
{
    public class UserRequestModel
    {
        public int RoleId { get; set; }

        public string? Email { get; set; }

        // Password should be encrypted before sending to API
        public string PasswordHash { get; set; }

        // Salt is from CryptographyHelper
        public string? Salt { get; set; }

        public int Gender { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsActive { get; set; }
    }
}
