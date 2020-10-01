namespace DigitalMenuApi.Dtos.AccountDtos
{
    public class AccountCreateDto
    {
        //id can't add
        //public int Id { get; set; }
        public string Token { get; set; }
        public int? StoreId { get; set; }
        public int RoleId { get; set; }


    }
}
