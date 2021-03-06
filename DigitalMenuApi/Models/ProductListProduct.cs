namespace DigitalMenuApi.Models
{
    public partial class ProductListProduct
    {
        public int Id { get; set; }
        public int? ProductListId { get; set; }
        public int? ProductId { get; set; }
        public int? Location { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductList ProductList { get; set; }
    }
}