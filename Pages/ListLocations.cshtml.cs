using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RPtest.Pages;
[Authorize]
public class ListLocationsModel(ApplicationDbContext _context) : PageModel
{
    public List<Location> Locations { get; set; } = new List<Location>();

    public void OnGet()
    {
        Locations = _context.Locations.Include(l => l.Vehicule).ThenInclude(v => v.Model).Include(l => l.Paiements).OrderByDescending(l => l.Id).ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
        {
            return NotFound();
        }

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListLocations");
    }
}
