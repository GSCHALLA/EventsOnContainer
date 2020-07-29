using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EventCatalogApi.Data;
using EventCatalogApi.Domain;
using EventCatalogApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace EventCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public EventController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet("{items}")]
        public async Task<IActionResult> Items([FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var itemsCount = await _context.EventDetails.LongCountAsync();
            var items = await _context.EventDetails
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            items = ChangePictureUrl(items);

            var model = new PaginatedItemsViewModel<EventDetails>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount,
                Data = items

            };


            return Ok(model);
        }

        private List<EventDetails> ChangePictureUrl(List<EventDetails> items)
        {
            items.ForEach(items => items.ImageUrl = items.ImageUrl.Replace(
                "http://externalcatalogbaseurltobereplaced", _config["ExternalCatalogBaseUrl"]));
            return items;
        }   
        [HttpGet("[action]")]
        public async Task<IActionResult> EventTypes()
        {
            var types = await _context.EventTypes.ToListAsync();
            return Ok(types);

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> EventLocations()
        {
            var locations = await _context.EventLocations
                .ToListAsync();
            return Ok(locations);

        }
        [HttpGet("[action]/type/{eventTypeId}/location/{eventLocationId}")]
        public async Task<IActionResult> Items(
            int? eventTypeId,
            int? eventLocationId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<EventDetails>)_context.EventDetails
                /*.Include(c => c.EventType)
                .Include(c => c.EventLocation)*/;

            if (eventTypeId.HasValue)
            {
                query = query.Where(c => c.EventTypeId == eventTypeId);
            }
            if (eventLocationId.HasValue)
            {
                query = query.Where(c => c.EventLocationId == eventLocationId);
            }

            var itemsCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            items = ChangePictureUrl(items);

            var model = new PaginatedItemsViewModel<EventDetails>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Count = itemsCount,
                Data = items
            };


            return Ok(model);


        }
        [HttpGet("[action]")]
        public async Task<IActionResult> LocationCountDetails()
        {
            var groups =  _context.EventDetails
                .Include(c => c.EventLocation)
                .Include(c => c.EventType)
                .AsEnumerable()
                .GroupBy(c => c.EventLocationId)
                ;
                
                

            //Console.WriteLine(groups);

            var detailsList = new List<EventLocationDetails>();
            foreach (var group in groups)
            {
                var eventDetails = new EventLocationDetails
                {
                    Id = group.Key,                   
                    Name = group.First().EventLocation.Location,
                    Count = group.Count()
                    //Type = group.All

                };
                Console.WriteLine(eventDetails.Id + eventDetails.Name + eventDetails.Count);
                detailsList.Add(eventDetails);

                    
                Console.WriteLine("Number of Events in " + group.Key + "  are " + group.Count());
                foreach (var items in group)
                    Console.WriteLine(items.EventTypeId);

            }
            return Ok(detailsList);
        }
    }

class EventLocationDetails
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<EventType> Type { get; set; }

    }
    
}
