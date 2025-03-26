using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class ListQuartierModel(ApplicationDbContext _context) : PageModel
{
    public List<Quartier> Quartiers { get; set; } = new();

    public async Task OnGetAsync()
    {
        Quartiers = await _context.Quartiers.Include(q => q.Ville).OrderBy(q => q.VilleId).ToListAsync();
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
