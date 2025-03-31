using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class ListDepenseModel(ApplicationDbContext _context) : PageModel
{
    public List<Depense> Depenses { get; set; } = new();
    public int VehiculeId { get; set; }
    public string VehiculeMarque { get; set; }
    public string VehiculeModel { get; set; }
    public string VehiculeImmatriculation { get; set; }

    public async Task OnGetAsync(int vehiculeId)
    {
        VehiculeId = vehiculeId;

        var vehicule = await _context.Vehicules.Include(v => v.Model)
            .FirstOrDefaultAsync(v => v.Id == vehiculeId);

        if (vehicule != null)
        {
            VehiculeMarque = vehicule.Model.Marque;
            VehiculeModel = vehicule.Model.Nom;
            VehiculeImmatriculation = vehicule.Immatriculation;
        }

        Depenses = await _context.Depenses
            .Where(d => d.VehiculeId == vehiculeId)
            .OrderByDescending(d => d.Id)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var depense = await _context.Depenses.FindAsync(id);
        if (depense == null)
        {
            return NotFound();
        }

        _context.Depenses.Remove(depense);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListDepense");
    }
}
