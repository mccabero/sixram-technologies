namespace Sixram.Models.Request
{
    public class CompanyRequestModel : BaseModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string TIN { get; set; }
    }
}
