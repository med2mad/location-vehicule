using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class AddTypeModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public TypeVehicule Type { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            Type = new TypeVehicule();
            return Page();
        }

        Type = await _context.Types.FirstOrDefaultAsync(m => m.Id == id);

        if (Type == null)
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

        if (Type.Id == 0)
        {
            _context.Types.Add(Type);
        }
        else
        {
            _context.Attach(Type).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("ListType");
    }
}
