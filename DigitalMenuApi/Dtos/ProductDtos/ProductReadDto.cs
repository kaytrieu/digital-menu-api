namespace DigitalMenuApi.Dtos.ProductDtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Src { get; set; }

        //need to include Store
        public string StoreName { get; set; }
    }
}
