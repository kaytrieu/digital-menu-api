
using System.ComponentModel.DataAnnotations;

namespace DigitalMenuApi.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int? StoreId { get; set; }
        public int RoleId { get; set; }

        public virtual AccountRole Role { get; set; }
        public virtual Store Store { get; set; }
    }
}