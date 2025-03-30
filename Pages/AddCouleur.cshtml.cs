using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class AddCouleurModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Couleur Couleur { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            Couleur = new Couleur();
            return Page();
        }

        Couleur = await _context.Couleurs.FirstOrDefaultAsync(m => m.Id == id);

        if (Couleur == null)
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

        if (Couleur.Id == 0)
        {
            _context.Couleurs.Add(Couleur);
        }
        else
        {
            _context.Attach(Couleur).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("ListCouleur");
    }
}
