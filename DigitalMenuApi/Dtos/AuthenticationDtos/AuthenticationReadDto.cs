using DigitalMenuApi.Dtos.AccountDtos;

namespace DigitalMenuApi.Dtos.AuthenticationDtos
{
    public class AuthenticationReadDto
    {
        public string Token { get; set; }
        public AccountReadAfterAuthenDto Account { get; set; }
    }
}
