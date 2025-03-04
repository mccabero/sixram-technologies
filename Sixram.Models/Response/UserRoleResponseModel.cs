namespace Sixram.Models.Response
{
    public class UserRoleResponseModel : BaseModel
    {
        public UserResponseModel Users { get; set; }
        public int UserId { get; set; }

        public RoleResponseModel Roles { get; set; }
        public int RoleId { get; set; }
    }
}
