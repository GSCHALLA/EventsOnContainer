using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModel;

namespace WebMvc.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }
        public async  Task<IActionResult> Index(int? page, int? locationFilterApplied,int? typesFilterApplied)
        {
            var itemsOnPage = 10;

            var events =  await _service.GetEventDetailsAsync(page ?? 0,itemsOnPage,locationFilterApplied,typesFilterApplied);

            var vm = new EventIndexViewModel
            {
                EventDetails = events.Data,
                Locations = await _service.GetLocationsAsync(),
                Types = await _service.GetEventTypesAsync(),
                PaginationInfo = new PaginationInfo
                {

                    ActualPage = page ?? 0,
                    ItemsPerPage = events.Data.Count,
                    TotalItems = events.Count,
                    TotalPages = (int)Math.Ceiling((Decimal)events.Count / itemsOnPage),

                },
                LocationFilterApplied  = locationFilterApplied ?? 0,
                TypesFilterApplied = typesFilterApplied ?? 0
            };
            return View(vm);
        }
    }
}
