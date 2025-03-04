namespace Sixram.DTO
{
    public class UsersDto : BaseDto
    {
        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
        public string? ConfirmPasswordHash { get; set; }

        public string? Salt { get; set; }

        public RolesDto Roles { get; set; }
        public int RoleId { get; set; }

        public int Gender { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsActive { get; set; }

        public virtual List<UserRolesDto>? UserRoles { get; set; }
    }
}