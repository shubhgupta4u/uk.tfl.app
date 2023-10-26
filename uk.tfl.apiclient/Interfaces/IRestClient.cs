using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.tfl.apiclient.Interfaces
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> query);
        Task<T> GetAsync<T>(string url);
    }
}
