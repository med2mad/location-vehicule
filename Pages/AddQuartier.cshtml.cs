using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class AddQuartierModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Quartier Quartier { get; set; } = new();

    public List<Ville> Villes { get; set; } = new();

    public async Task OnGetAsync()
    {
        Villes = await _context.Villes.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Quartiers.Add(Quartier);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListQuartier");
    }
}
