using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class ProductList
    {
        public ProductList()
        {
            ProductListProduct = new HashSet<ProductListProduct>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? BoxId { get; set; }
        public int? MaxSize { get; set; }
        public bool? IsAvailable { get; set; }
        public int? Location { get; set; }

        public virtual Box Box { get; set; }
        public virtual ICollection<ProductListProduct> ProductListProduct { get; set; }
    }
}