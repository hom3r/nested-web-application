using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using child.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace child.Services
{
    public class PageService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IConfiguration configuration;

        public PageService(IHttpClientFactory _clientFactory,
                            IConfiguration _configuration)
        {
            clientFactory = _clientFactory;
            configuration = _configuration;
        }

        public string GetParentHost()
        {
            return configuration["ParentHost"];
        }

        public async Task<UserPage> GetPageByHash(string hash)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}/api/preview/{1}", configuration["ParentHost"], hash));

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<UserPage>(responseStream);
            }
            else
            {
                return null;
            }
        }

        // TODO get data by GET request to the parent app
    }
}
