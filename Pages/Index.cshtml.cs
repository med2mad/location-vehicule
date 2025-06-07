using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class IndexModel(ApplicationDbContext _context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();
    public List<string> Marques { get; set; } = new();
    public Location Location { get; set; } //just for dd/MM/yyyy format

    public async void OnGet()
    {
        Vehicules = await _context.Vehicules.Include(v => v.Model).Take(8).OrderByDescending(v => v.Id).ToListAsync();
        Marques = _context.Models.Select(m => m.Marque).Distinct().ToList();

    }
}
