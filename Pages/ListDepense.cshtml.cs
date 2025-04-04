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

    public void OnGet(int vehiculeId)
    {
        VehiculeId = vehiculeId;

        var vehicule = _context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == vehiculeId);

        if (vehicule != null)
        {
            VehiculeMarque = vehicule.Model.Marque;
            VehiculeModel = vehicule.Model.Nom;
            VehiculeImmatriculation = vehicule.Immatriculation;
        }

        Depenses = _context.Depenses.Where(d => d.VehiculeId == vehiculeId).OrderByDescending(d => d.Id).ToList();
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
