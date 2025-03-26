using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class ListeVillesModel(ApplicationDbContext _context) : PageModel
{
    public List<Ville> Villes { get; set; } = new List<Ville>();

    public async Task OnGetAsync()
    {
        Villes = await _context.Villes.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var ville = await _context.Villes.FindAsync(id);
        if (ville != null)
        {
            _context.Villes.Remove(ville);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
