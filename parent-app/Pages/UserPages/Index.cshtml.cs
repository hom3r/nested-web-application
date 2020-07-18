using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using parent.Data;
using parent.Models;

namespace parent.Pages.UserPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly parent.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(parent.Data.ApplicationDbContext context,
                          UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<UserPage> UserPage { get;set; }

        public async Task OnGetAsync()
        { 
            // set current user as the owner
            var user = await _userManager.GetUserAsync(HttpContext.User);

            UserPage = await _context.UserPage
                .Where(x => x.Owner.Equals(user))
                .ToListAsync();
        }
    }
}
