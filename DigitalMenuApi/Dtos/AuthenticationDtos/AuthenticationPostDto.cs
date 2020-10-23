using System.ComponentModel.DataAnnotations;

namespace DigitalMenuApi.Dtos.AuthenticationDtos
{
    public class AuthenticationPostDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
