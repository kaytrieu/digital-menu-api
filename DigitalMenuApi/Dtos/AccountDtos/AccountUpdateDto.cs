namespace DigitalMenuApi.Dtos.AccountDtos
{
    public class AccountUpdateDto
    {
        //id can't modify
        //public int Id { get; set; }
        public int? StoreId { get; set; }
        public int RoleId { get; set; }
    }
}
