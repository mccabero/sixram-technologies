namespace Sixram.Models.Response
{
    public class UserResponseModel : BaseModel
    {
        public RoleResponseModel Roles { get; set; }
        public int RoleId { get; set; }

        public string? Email { get; set; }

        public int Gender { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsActive { get; set; }
    }
}
