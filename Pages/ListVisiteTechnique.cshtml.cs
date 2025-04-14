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

    [BindProperty(SupportsGet = true)] public string Sort { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? StartDate { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? EndDate { get; set; }

    public async Task OnGetAsync(int vehiculeId)
    {
        VehiculeId = vehiculeId;

        DateTime sd = DateTime.MinValue;
        DateTime ed = DateTime.MaxValue;
        if (StartDate.HasValue)
        {
            sd = StartDate.Value;
        }
        if (EndDate.HasValue)
        {
            ed = EndDate.Value.AddDays(1).AddTicks(-1); // Add a day to include the entire end date (time = 00:00:00)
        }

        var vehicule = await _context.Vehicules.Include(v => v.Model).FirstOrDefaultAsync(v => v.Id == vehiculeId);

        if (vehicule != null)
        {
            VehiculeMarque = vehicule.Model.Marque;
            VehiculeModel = vehicule.Model.Nom;
            VehiculeImmatriculation = vehicule.Immatriculation;
        }

        var query = _context.VisitesTechniques.Where(d => d.VehiculeId == vehiculeId && d.Date >= sd && d.Date < ed);

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

        VisitesTechniques = await query.ToListAsync();
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
