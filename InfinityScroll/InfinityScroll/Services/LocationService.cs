using InfinityScroll.Helper;
using InfinityScroll.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfinityScroll.Services
{
    public class LocationService
    {
        public static async Task<List<LocationsModel>> GetLocations(int PageIndex, int PAGE_SIZE)
        {
            List<LocationsModel> result = new List<LocationsModel>();
            try
            {
                using (HttpClient client = new HttpClient { BaseAddress = new Uri("http://www.abc.com/") })
                {
                    string requestUri = String.Format("/api/locations?aid=getbyrecent");
                    HttpResponseMessage response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<LocationsModel>>(res);
                    }

                    result = result.Skip(PageIndex * PAGE_SIZE).Take(PAGE_SIZE).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to get login info.", ex);
            }
        }
    }
}
