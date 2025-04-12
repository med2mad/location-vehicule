using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class ListNotificationModel(ApplicationDbContext _context) : PageModel
{
    public IList<Notification> Notifications { get; set; }

    public async Task OnGetAsync()
    {
        Notifications = await _context.Notifications.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/ListNotification");
    }
}
