using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using parent.Data;
using parent.Models;

namespace parent.Pages.UserPages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly parent.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(parent.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserPage UserPage { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // set current user as the owner
            // TODO is this the best approach?
            UserPage.Owner = await _userManager.GetUserAsync(HttpContext.User);

            _context.UserPage.Add(UserPage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
