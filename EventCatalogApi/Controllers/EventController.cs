using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly CatalogContext _context;
        public EventController(CatalogContext context)
        {
            _context = context;
        }
        [HttpGet("{items}")]
        public  async Task<IActionResult> Items([FromQuery]int pageIndex = 0,
            [FromQuery]int pageSize = 6)
        {
            var items = await _context.EventDetails
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(items);
        }
    }
}
