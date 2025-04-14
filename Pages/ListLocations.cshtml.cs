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

    [BindProperty(SupportsGet = true)] public DateTime? StartDate { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? EndDate { get; set; }
    [BindProperty(SupportsGet = true)] public string Immatriculation { get; set; }
    [BindProperty(SupportsGet = true)] public string Sort { get; set; }

    public void OnGet()
    {
        var today = DateTime.Today;
        DateTime sd = new DateTime(today.Year, 1, 1);
        DateTime ed = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
        ed = ed.Date.AddDays(1).AddTicks(-1); //Add a day to include the entire end date (time = 00:00:00)

        if (StartDate.HasValue)
        {
            sd = StartDate.Value;
        }
        else
        {
            sd = DateTime.MinValue;
        }
        if (EndDate.HasValue)
        {
            ed = EndDate.Value.AddDays(1).AddTicks(-1); //Add a day to include the entire end date (time = 00:00:00)
        }
        else
        {
            ed = DateTime.MaxValue;
        }

        var query = _context.Locations.Include(l => l.Vehicule).ThenInclude(v => v.Model).Include(l => l.Paiements).Where(l => l.Date >= sd && l.Date < ed);

        if (!string.IsNullOrEmpty(Immatriculation))
        {
            query = query.Where(l => l.Vehicule.Immatriculation.Contains(Immatriculation));
        }

        if (!string.IsNullOrEmpty(Sort))
        {
            if (Sort == "Date")
            {
                query = query.OrderByDescending(l=>l.Date);
            }
            else if (Sort == "Montant")
            {
                query = query.OrderByDescending(l => l.Paiements.FirstOrDefault().Montant);
            }
        }
        else
        {
            query = query.OrderByDescending(l => l.Id);
        }

        Locations = query.ToList();
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

        return RedirectToPage("/ListLocations");
    }
}
