using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Layout
    {
        public Layout()
        {
            DesignBox = new HashSet<DesignBox>();
            Template = new HashSet<Template>();
        }

        public int Id { get; set; }
        public string AspectRatio { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DesignBox> DesignBox { get; set; }
        public virtual ICollection<Template> Template { get; set; }
    }
}