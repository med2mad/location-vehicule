using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages
{
    public class AddVilleModel(ApplicationDbContext _context) : PageModel
    {
        [BindProperty]
        public Ville Ville { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Villes.Add(Ville);
            await _context.SaveChangesAsync();

            return RedirectToPage("ListeVilles");
        }
    }
}
