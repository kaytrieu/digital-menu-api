namespace DigitalMenuApi.Dtos.BoxDtos
{
    public class BoxReadDto
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public int? Location { get; set; }
        public int? BoxTypeId { get; set; }
        public string Src { get; set; }
        public string HeaderTitle { get; set; }
        public string FooterTitle { get; set; }
        public string HeaderSrc { get; set; }
        public string FooterSrc { get; set; }

    }
}
