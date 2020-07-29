using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public static class ApiPaths
    {
        public static class Event
        {
            public static string GetEventTypes(string baseUri)
            {
                return $"{baseUri}eventtypes";
            }
            public static string GetEventLocations(string baseUri)
            {
                return $"{baseUri}eventlocations";
            }
            public static string GetAllEventDetails(string baseUri,int page,int take,int? location,int? type)
            {
                var filterQs = string.Empty;
                if(location.HasValue || type.HasValue)
                {
                    var locationQs = (location.HasValue) ? location.Value.ToString() : "";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "";
                     filterQs = $"/type/{typeQs}/location/{locationQs}";
                }
                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }
        }
    }
}
