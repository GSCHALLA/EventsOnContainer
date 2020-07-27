using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string baseUri, string authorizationToken = null,
            string authorizationMethod = "Bearer");
    }
}
