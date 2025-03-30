using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class ListTypeModel(ApplicationDbContext _context) : PageModel
{
    public IList<TypeVehicule> Types { get; set; }

    public async Task OnGetAsync()
    {
        Types = await _context.Types.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var type = await _context.Types.FindAsync(id);
        if (type == null)
        {
            return NotFound();
        }

        _context.Types.Remove(type);
        await _context.SaveChangesAsync();

        return RedirectToPage("ListType");
    }
}
