
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int? StoreId { get; set; }
        public int RoleId { get; set; }
        public bool? IsAvailable { get; set; }
        public string Password { get; set; }

        public virtual AccountRole Role { get; set; }
        public virtual Store Store { get; set; }
    }
}