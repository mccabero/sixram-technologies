using System.ComponentModel.DataAnnotations;

namespace Sixram.Models.Response
{
    public class LoginResponseModel
    {
        public LoginResponseModel(bool isAuthenticated, string token, string errorMessage = "")
        {
            IsAuthenticated = isAuthenticated;
            Token = token;
            ErrorMessage = errorMessage;
        }

        public bool IsAuthenticated { get; set; }

        public string Token { get; set; }

        public string ErrorMessage { get; set; }
    }
}
