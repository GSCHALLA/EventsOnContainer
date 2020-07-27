using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventService : IEventService
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;
        public EventService(IConfiguration config, IHttpClient client)  
        {
            _baseUrl = $"{config["EventUrl"]}/api/event/";
            _client = client;
        }
        public async Task<Event> GetEventDetailsAsync(int page, int size, int? location, int? type)
        {
            var eventDetailsUrl = ApiPaths.Event.GetAllEventDetails(_baseUrl, page, size, location, type);
            var dataString = await _client.GetStringAsync(eventDetailsUrl);
            return JsonConvert.DeserializeObject<Event>(dataString);
        }

        public async Task<IEnumerable<SelectListItem>> GetLocationsAsync()
        {
            var eventLocationUri = ApiPaths.Event.GetEventLocations(_baseUrl);
            var dataString = await _client.GetStringAsync(eventLocationUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                Value = null,
                Text = "All",
                Selected = true
            }
        };

            var locations = JArray.Parse(dataString);
            foreach (var location in locations)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = location.Value<string>("id"),
                        Text = location.Value<string>("location")


                    });
            }
            return items;
        }
        public async Task<IEnumerable<SelectListItem>> GetEventTypesAsync()
        {
            var eventTypeUri = ApiPaths.Event.GetEventTypes(_baseUrl);
            var dataString = await _client.GetStringAsync(eventTypeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                Value = null,
                Text = "All",
                Selected = true
            }
        };

            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")


                    });
            }
            return items;
        }

       
    }
}
