using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class AddDepenseModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Depense Depense { get; set; }

    [BindProperty]
    public bool IsNew { get; set; } = true;

    public int VehiculeId { get; set; }
    public string VehiculeMarque { get; set; }
    public string VehiculeModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, int vehiculeId)
    {
        VehiculeId = vehiculeId;

        var vehicule = await _context.Vehicules.Include(v => v.Model).FirstOrDefaultAsync(v => v.Id == vehiculeId);
        if (vehicule != null)
        {
            VehiculeMarque = vehicule.Model.Marque;
            VehiculeModel = vehicule.Model.Nom;
        }

        if (id == null)
        {
            // Mode cr�ation
            IsNew = true;
            Depense = new Depense
            {
                VehiculeId = vehiculeId,
                Date = DateTime.Today
            };
        }
        else
        {
            // Mode �dition
            IsNew = false;
            Depense = await _context.Depenses.FirstOrDefaultAsync(m => m.Id == id);

            if (Depense == null)
            {
                return NotFound();
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (IsNew)
        {
            // Ajout d'une nouvelle d�pense
            _context.Depenses.Add(Depense);
        }
        else
        {
            // Modification d'une d�pense existante
            _context.Attach(Depense).State = EntityState.Modified;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepenseExists(Depense.Id) && !IsNew)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("/ListDepense", new { vehiculeId = Depense.VehiculeId });
    }

    private bool DepenseExists(int id)
    {
        return _context.Depenses.Any(e => e.Id == id);
    }
}
