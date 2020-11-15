using BCrypt.Net;

namespace DigitalMenuApi.Dtos.AccountDtos
{
    public class AccountCreateDto
    {
        public AccountCreateDto()
        {
            Salt = BCrypt.Net.BCrypt.GenerateSalt();
        }
        //id can't add
        //public int Id { get; set; }
        public string Username { get; set; }
        public int? StoreId { get; set; }
        public int RoleId { get; set; }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = BCrypt.Net.BCrypt.HashPassword(value,Salt); }
        }

        public string Salt { get;}
    }
}
