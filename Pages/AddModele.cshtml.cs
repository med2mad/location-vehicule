using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class AddModeleModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Model Modele { get; set; }

    public List<string> Marques { get; set; } = new();
    public List<TypeVehicule> Types { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Marques = _context.Models.Select(m => m.Marque).Distinct().ToList();
        Types = _context.Types.ToList();

        if (id == null)
        {
            Modele = new Model();
            return Page();
        }

        Modele = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);

        if (Modele == null)
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

        if (Modele.Id == 0)
        {
            _context.Models.Add(Modele);
        }
        else
        {
            _context.Attach(Modele).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("ListModele");
    }
}
