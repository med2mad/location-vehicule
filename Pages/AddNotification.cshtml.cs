using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class AddNotificationModel(ApplicationDbContext _context) : PageModel
{

    [BindProperty]
    public Notification Notification { get; set; }

    public bool IsEditMode { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            // Mode création
            IsEditMode = false;
            Notification = new Notification();
        }
        else
        {
            // Mode édition
            IsEditMode = true;
            Notification = await _context.Notifications.FindAsync(id);

            if (Notification == null)
            {
                return NotFound();
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Notification.Jours.HasValue && !Notification.Mois.HasValue && !Notification.Annees.HasValue)
        {
            ModelState.AddModelError(string.Empty, "Donnez une période");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        bool any = await _context.Notifications.AnyAsync(n => n.Titre == Notification.Titre && n.Id != Notification.Id);
        if (any || Notification.Titre== "Visites Techniques" || Notification.Titre == "Vidanges" || Notification.Titre == "Visite Technique" || Notification.Titre == "Vidange")
        {
            ModelState.AddModelError("Notification.Titre", "Notification déjà existante");
            return Page();
        }

        if (Notification.Id == 0)
        {
            // Ajout
            _context.Notifications.Add(Notification);
        }
        else
        {
            // Modification
            _context.Attach(Notification).State = EntityState.Modified;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotificationExists(Notification.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("/ListNotification");
    }

    private bool NotificationExists(int id)
    {
        return _context.Notifications.Any(e => e.Id == id);
    }
}
