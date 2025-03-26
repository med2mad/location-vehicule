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

    public List<Ville> Villes { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Quartier = await _context.Quartiers.FindAsync(id);
        if (Quartier == null)
        {
            return NotFound();
        }

        Villes = await _context.Villes.ToListAsync();
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
