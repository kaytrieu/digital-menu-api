using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Store
    {
        public Store()
        {
            Account = new HashSet<Account>();
            Product = new HashSet<Product>();
            Screen = new HashSet<Screen>();
            Template = new HashSet<Template>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Screen> Screen { get; set; }
        public virtual ICollection<Template> Template { get; set; }
    }
}