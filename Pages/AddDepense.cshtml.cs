using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class AddDepenseModel(ApplicationDbContext _context) : PageModel
{

    [BindProperty]
    public Depense Depense { get; set; }
    [BindProperty]
    public bool IsNew { get; set; } = true;

    public Notification Notification { get; set; }

    public int VehiculeId { get; set; }
    public int NotificationId { get; set; }

    public Vehicule Vehicule { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, int vehiculeId, int notificationId)
    {
        VehiculeId = vehiculeId;
        NotificationId = notificationId;

        Vehicule = await _context.Vehicules.Include(v => v.Model).FirstOrDefaultAsync(v => v.Id == vehiculeId);

        Notification = await _context.Notifications.FirstOrDefaultAsync(v => v.Id == NotificationId);

        if (id == null)
        {
            // Mode création
            IsNew = true;
            Depense = new Depense
            {
                VehiculeId = vehiculeId,
                Date = DateTime.Today
            };
        }
        else
        {
            // Mode édition
            IsNew = false;
            Depense = await _context.Depenses.FirstOrDefaultAsync(m => m.Id == id);

            if (Depense == null)
            {
                return NotFound();
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int notificationId)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Depense.NotificationId = notificationId;

        if (IsNew)
        {
            // Ajout d'une nouvelle dépense
            _context.Depenses.Add(Depense);
        }
        else
        {
            // Modification d'une dépense existante
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

        return RedirectToPage("/ListDepense", new { vehiculeId = Depense.VehiculeId, notificationId = notificationId });
    }

    private bool DepenseExists(int id)
    {
        return _context.Depenses.Any(e => e.Id == id);
    }

}
