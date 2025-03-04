namespace Sixram.DTO
{
    public class UserRolesDto : BaseDto
    {
        public UsersDto Users { get; set; }
        public int UserId { get; set; }

        public RolesDto Roles { get; set; }
        public int RoleId { get; set; }
    }
}