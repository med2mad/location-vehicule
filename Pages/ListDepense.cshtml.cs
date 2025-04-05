using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class ListDepenseModel(ApplicationDbContext _context) : PageModel
{
    public List<Depense> Depenses { get; set; } = new();
    public int VehiculeId { get; set; }
    public string VehiculeMarque { get; set; }
    public string VehiculeModel { get; set; }
    public string VehiculeImmatriculation { get; set; }

    [BindProperty(SupportsGet = true)] public string Sort { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? StartDate { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? EndDate { get; set; }

    public void OnGet(int vehiculeId)
    {
        VehiculeId = vehiculeId;

        var today = DateTime.Today;
        DateTime sd = new DateTime(today.Year, 1, 1);
        DateTime ed = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
        ed = ed.Date.AddDays(1).AddTicks(-1);// Add a day to include the entire end date (time = 00:00:00)
        if (StartDate.HasValue)
        {
            sd = StartDate.Value;
        }
        else
        {
            StartDate = sd;
        }
        if (EndDate.HasValue)
        {
            ed = EndDate.Value.AddDays(1).AddTicks(-1); // Add a day to include the entire end date (time = 00:00:00)
        }
        else
        {
            EndDate = ed;
        }

        var vehicule = _context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == vehiculeId);

        if (vehicule != null)
        {
            VehiculeMarque = vehicule.Model.Marque;
            VehiculeModel = vehicule.Model.Nom;
            VehiculeImmatriculation = vehicule.Immatriculation;
        }

        var query = _context.Depenses.Where(d => d.VehiculeId == vehiculeId && d.Date >= sd && d.Date < ed);

        if (!string.IsNullOrEmpty(Sort))
        {
            if (Sort == "Date")
            {
                query = query.OrderByDescending(d => d.Date);
            }
            else if (Sort == "Montant")
            {
                query = query.OrderByDescending(d => d.Montant);
            }
        }
        else
        {
            query = query.OrderByDescending(d => d.Id);
        }

        Depenses = query.ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, int vehiculeId)
    {
        var depense = await _context.Depenses.FindAsync(id);
        if (depense == null)
        {
            return NotFound();
        }

        _context.Depenses.Remove(depense);
        await _context.SaveChangesAsync();

        return RedirectToPage(new { vehiculeId = vehiculeId });
    }
}
