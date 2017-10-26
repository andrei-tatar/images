﻿using System;
using Common;

namespace Images.Service.Entities
{
    public class Image : IEntity
    {
        public string Location { get; set; }
        public string[] Tags { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
