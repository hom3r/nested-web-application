using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using parent.Data;
using parent.Models;
using parent.Services;

namespace parent.Pages.UserPages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PreviewService previewService;
        private readonly IConfiguration configuration;

        public DetailsModel(ApplicationDbContext context, 
                            PreviewService _previewService,
                            IConfiguration _configuration)
        {
            _context = context;
            previewService = _previewService;
            configuration = _configuration;
        }

        public UserPage UserPage { get; set; }
        public IList<UserPagePreview> Previews { get; set; }
        public string ChildHost { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // code before
            //UserPage = await _context.UserPage.FirstOrDefaultAsync(m => m.ID == id);

            // code moved to the service
            UserPage = await previewService.GetUserPage((int)id);
            Previews = previewService.GetPreviews((int)id);
            ChildHost = configuration["ChildHost"];

            //Previews = await _context.UserPagePreview
            //    .Where(x => x.Page.Equals(UserPage))
            //    .ToListAsync();

            if (UserPage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
