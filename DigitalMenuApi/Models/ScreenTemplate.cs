namespace DigitalMenuApi.Models
{
    public partial class ScreenTemplate
    {
        public int Id { get; set; }
        public int ScreenId { get; set; }
        public int TemplateId { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Screen Screen { get; set; }
        public virtual Template Template { get; set; }
    }
}