﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Models
{
    public partial class Template
    {
        public Template()
        {
            Box = new HashSet<Box>();
            ScreenTemplate = new HashSet<ScreenTemplate>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool? IsAvailable { get; set; }
        public string Uilink { get; set; }
        public string Name { get; set; }
        public DateTime? LastModified { get; set; }
        public string Tags { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Box> Box { get; set; }
        public virtual ICollection<ScreenTemplate> ScreenTemplate { get; set; }
    }
}