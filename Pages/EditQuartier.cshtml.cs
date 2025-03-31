using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class EditQuartierModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Quartier Quartier { get; set; } = new();

    public List<string> Villes { get; set; } = new();
    public List<Quartier> Quartiers { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Quartier = await _context.Quartiers.FindAsync(id);
        if (Quartier == null)
        {
            return NotFound();
        }

        Quartiers = await _context.Quartiers.ToListAsync();
        Villes = await _context.Quartiers.Select(q => q.Ville).Distinct().ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Quartiers.Update(Quartier);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListQuartier");
    }
}
