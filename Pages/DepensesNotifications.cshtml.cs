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
    
    public async Task OnGetAsync()
    {
        Vehicule = await _context.Vehicules.FindAsync(vehiculeId);
        Notifications = await _context.Notifications.ToListAsync();
    }
}
