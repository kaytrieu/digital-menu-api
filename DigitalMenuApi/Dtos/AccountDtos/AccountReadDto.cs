﻿namespace DigitalMenuApi.Dtos.AccountDtos
{
    public class AccountReadDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string StoreName { get; set; }
        public string RoleName { get; set; }


        //public virtual AccountRole Role { get; set; }
        //public virtual Store Store { get; set; }


    }
}
