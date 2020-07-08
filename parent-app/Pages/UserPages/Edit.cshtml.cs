using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using parent.Data;
using parent.Models;

namespace parent.Pages.UserPages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly parent.Data.ApplicationDbContext _context;

        public EditModel(parent.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPageExists(UserPage.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserPageExists(int id)
        {
            return _context.UserPage.Any(e => e.ID == id);
        }
    }
}
