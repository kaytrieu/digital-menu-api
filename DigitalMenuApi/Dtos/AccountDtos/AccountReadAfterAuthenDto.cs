namespace DigitalMenuApi.Dtos.AccountDtos
{
    public class AccountReadAfterAuthenDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int RoleId { get; set; }


        //public virtual AccountRole Role { get; set; }
        //public virtual Store Store { get; set; }


    }
}
