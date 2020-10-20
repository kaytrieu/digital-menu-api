
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Screen
    {
        public Screen()
        {
            ScreenTemplate = new HashSet<ScreenTemplate>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Uid { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<ScreenTemplate> ScreenTemplate { get; set; }
    }
}