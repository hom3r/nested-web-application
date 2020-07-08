using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using parent.Models;
using parent.Services;

namespace parent
{
    [Route("api/preview")]
    public class ApiController : Controller
    {
        PreviewService previewService;

        public ApiController(PreviewService _previewService)
        {
            previewService = _previewService;
        }

        // GET api/preview/5
        //[HttpGet("{id}")]
        //public async Task<string> GetAsync(int id)
        //{
        //    UserPage userPage = await previewService.GetUserPage(id);
        //    return JsonSerializer.Serialize(userPage);
        //}

        // GET api/preview/5
        [HttpGet("{hash}")]
        public async Task<string> GetAsync(string hash)
        {
            UserPage userPage = await previewService.GetUserPageByHash(hash);
            return JsonSerializer.Serialize(userPage);
        }

        // PUT api/preview/5
        [HttpPut("{pageId:int}")]
        public string Put(int pageId,
                        [FromBody] UserPage page)
        {
            UserPage newPage = previewService.UpdateUserPage(pageId, page.Name, page.Content);
            if (newPage == null)
            {
                // TODO set response code
                return "{ \"message\": \"Preview not found\" }";
            }
            return JsonSerializer.Serialize(page);
        }
    }
}
