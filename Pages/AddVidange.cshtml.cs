using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class AddVidangeModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Vidange Vidange { get; set; }

    [BindProperty]
    public bool IsNew { get; set; } = true;

    public Vehicule Vehicule { get; set; }

    public int VehiculeId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, int vehiculeId)
    {
        VehiculeId = vehiculeId;

        Vehicule = await _context.Vehicules.Include(v => v.Model).FirstOrDefaultAsync(v => v.Id == vehiculeId);

        if (id == null)
        {
            // Mode création
            IsNew = true;
            Vidange = new Vidange
            {
                VehiculeId = vehiculeId,
                Date = DateTime.Today
            };
        }
        else
        {
            // Mode édition
            IsNew = false;
            Vidange = await _context.Vidanges.FirstOrDefaultAsync(m => m.Id == id);

            if (Vidange == null)
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
            // Ajout d'une nouvelle dépense
            _context.Vidanges.Add(Vidange);
        }
        else
        {
            // Modification d'une dépense existante
            _context.Attach(Vidange).State = EntityState.Modified;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepenseExists(Vidange.Id) && !IsNew)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("/ListVidange", new { vehiculeId = Vidange.VehiculeId });
    }

    private bool DepenseExists(int id)
    {
        return _context.Vidanges.Any(e => e.Id == id);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var vidange = await _context.Vidanges.FindAsync(id);
        if (vidange == null)
        {
            return NotFound();
        }

        _context.Vidanges.Remove(vidange);
        await _context.SaveChangesAsync();

        return RedirectToPage("/ListVidange");
    }
}
