using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class DepensesNotificationsModel(ApplicationDbContext _context) : PageModel
{
    public IList<Notification> Notifications { get; set; }
    public Vehicule Vehicule { get; set; }
    [BindProperty(SupportsGet = true)] public int vehiculeId { get; set; }
    public List<Vidange> lastVidanges { get; set; } = new List<Vidange>();
    public List<VisiteTechnique> lastVisiteTechniques { get; set; } = new List<VisiteTechnique>();
    public List<Depense> lastDepenses { get; set; } = new List<Depense>();

    public async Task OnGetAsync()
    {
        Vehicule = await _context.Vehicules.FindAsync(vehiculeId);
        Notifications = await _context.Notifications.ToListAsync();

        lastVidanges = await _context.Vidanges.OrderByDescending(x => x.Date).ToListAsync();
        lastVisiteTechniques = await _context.VisitesTechniques.OrderByDescending(x => x.Date).ToListAsync();
        lastDepenses = await _context.Depenses.OrderByDescending(x => x.Date).ToListAsync();
    }
}
