using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class DetailsVehiculeModel(ApplicationDbContext _context) : PageModel
{
    public Vehicule Vehicule { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Vehicule = await _context.Vehicules.Include(v => v.Model).Include(v => v.Depenses).Include(v => v.VisitesTechniques).Include(v => v.Vidanges).FirstOrDefaultAsync(m => m.Id == id);

        if (Vehicule == null)
        {
            return NotFound();
        }

        return Page();
    }
}
