using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class DeleteLocationsModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Location Location { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Location = await _context.Locations.FindAsync(id);

        if (Location == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);

        if (location != null)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("ListLocations");
    }
}
