using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModel
{
    public class EventIndexViewModel
    {
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<EventDetails> EventDetails { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public int? LocationFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
    }
}
