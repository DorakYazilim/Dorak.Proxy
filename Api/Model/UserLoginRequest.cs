using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}