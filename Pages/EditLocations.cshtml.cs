using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class EditLocationsModel(ApplicationDbContext _context) : PageModel
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Locations.Update(Location);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListLocations");
    }
}
