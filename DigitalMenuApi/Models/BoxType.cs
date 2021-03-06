using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class BoxType
    {
        public BoxType()
        {
            Box = new HashSet<Box>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual ICollection<Box> Box { get; set; }
    }
}