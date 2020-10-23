namespace DigitalMenuApi.Models
{
    public partial class DesignBox
    {
        public int Id { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public int? OffsetX { get; set; }
        public int? OffsetY { get; set; }
        public int? LayoutId { get; set; }

        public virtual Layout Layout { get; set; }
    }
}