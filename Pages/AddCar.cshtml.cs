using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;

namespace RPtest.Pages;

public class AddCarModel(ApplicationDbContext _context) : PageModel
{
    public List<string> Marques { get; set; } = new();

    public async void OnGet()
    {
        Marques = await _context.Models.Select(m => m.Marque).Distinct().ToListAsync();
    }
}
