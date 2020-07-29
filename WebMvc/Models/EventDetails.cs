﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public String Age { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Venue { get; set; }
        public String Date { get; set; }
        public int EventTypeId { get; set; }
        public  string EventType { get; set; }
        public int EventLocationId { get; set; }
        public  string EventLocation { get; set; }
    }
}
