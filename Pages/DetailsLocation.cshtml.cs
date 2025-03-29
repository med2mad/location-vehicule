using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class DetailsLocationModel(ApplicationDbContext _context) : PageModel
{
    public Location Location { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Location = await _context.Locations.Include(l => l.Paiement).Include(l => l.Vehicule).ThenInclude(l => l.Model).FirstOrDefaultAsync(m => m.Id == id);

        if (Location == null)
        {
            return NotFound();
        }

        return Page();
    }
}