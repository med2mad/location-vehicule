using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class ListCouleurModel(ApplicationDbContext _context) : PageModel
{
    public IList<Couleur> Couleurs { get; set; }

    public async Task OnGetAsync()
    {
        Couleurs = await _context.Couleurs.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var couleur = await _context.Couleurs.FindAsync(id);
        if (couleur == null)
        {
            return NotFound();
        }

        _context.Couleurs.Remove(couleur);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListCouleur");
    }
}
