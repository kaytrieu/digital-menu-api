
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Box
    {
        public Box()
        {
            ProductList = new HashSet<ProductList>();
        }

        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int? Location { get; set; }
        public int? BoxTypeId { get; set; }
        public string Src { get; set; }
        public string HeaderTitle { get; set; }
        public string FooterTitle { get; set; }
        public string HeaderSrc { get; set; }
        public string FooterSrc { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual BoxType BoxType { get; set; }
        public virtual Template Template { get; set; }
        public virtual ICollection<ProductList> ProductList { get; set; }
    }
}