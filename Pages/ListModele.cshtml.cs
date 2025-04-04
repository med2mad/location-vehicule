using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class ListModeleModel(ApplicationDbContext _context) : PageModel
{
    public IList<Model> Models { get; set; }

    public async Task OnGetAsync()
    {
        Models = await _context.Models.OrderBy(m => m.Marque).ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var modele = await _context.Models.FindAsync(id);
        if (modele == null)
        {
            return NotFound();
        }

        _context.Models.Remove(modele);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListModele");
    }
}
