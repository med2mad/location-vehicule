using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class ListVisiteTechniqueModel(ApplicationDbContext _context) : PageModel
{
    public List<VisiteTechnique> VisitesTechniques { get; set; } = new List<VisiteTechnique>();
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

        VisitesTechniques = await _context.VisitesTechniques
            .Where(d => d.VehiculeId == vehiculeId)
            .OrderByDescending(d => d.Id)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, int vehiculeId)
    {
        var visiteTechnique = await _context.VisitesTechniques.FindAsync(id);
        if (visiteTechnique == null)
        {
            return NotFound();
        }

        _context.VisitesTechniques.Remove(visiteTechnique);
        await _context.SaveChangesAsync();

        return RedirectToPage(new { vehiculeId = vehiculeId });
    }
}
