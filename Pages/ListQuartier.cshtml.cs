using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class ListQuartierModel(ApplicationDbContext _context) : PageModel
{
    public List<Quartier> Quartiers { get; set; } = new();

    public async Task OnGetAsync()
    {
        Quartiers = await _context.Quartiers.OrderBy(q => q.Ville).ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var quartier = await _context.Quartiers.FindAsync(id);
        if (quartier != null)
        {
            _context.Quartiers.Remove(quartier);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
