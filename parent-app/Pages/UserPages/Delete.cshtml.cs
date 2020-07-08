using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using parent.Data;
using parent.Models;

namespace parent.Pages.UserPages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly parent.Data.ApplicationDbContext _context;

        public DeleteModel(parent.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserPage UserPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserPage = await _context.UserPage.FirstOrDefaultAsync(m => m.ID == id);

            if (UserPage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserPage = await _context.UserPage.FindAsync(id);

            if (UserPage != null)
            {
                _context.UserPage.Remove(UserPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
