namespace DigitalMenuApi.Dtos.ProductDtos
{
    public class ProductUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Src { get; set; }
        public int? StoreId { get; set; }
    }
}
