using System.ComponentModel.DataAnnotations;

namespace DigitalMenuApi.Dtos.AccountDtos
{
    public class AccountLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
