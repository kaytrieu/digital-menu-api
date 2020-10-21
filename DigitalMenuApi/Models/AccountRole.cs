
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class AccountRole
    {
        public AccountRole()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}