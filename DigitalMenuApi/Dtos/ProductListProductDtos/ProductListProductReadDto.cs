namespace DigitalMenuApi.Dtos.ProductListProductDtos
{
    public class ProductListProductReadDto
    {
        public int Id { get; set; }
        public int? ProductListId { get; set; }
        public int? ProductId { get; set; }
        public int? Location { get; set; }
    }
}
