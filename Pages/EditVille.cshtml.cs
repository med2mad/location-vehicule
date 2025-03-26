using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class EditVilleModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Ville Ville { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Ville = await _context.Villes.FindAsync(id);
        if (Ville == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Villes.Update(Ville);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListeVilles");
    }
}
