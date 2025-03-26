using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class CarsList(ApplicationDbContext _context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();

    public void OnGet()
    {
        Vehicules = _context.Vehicules.Include(v => v.Model).ToList();
    }
}
