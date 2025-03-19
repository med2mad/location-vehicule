using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class IndexModel(ApplicationDbContext context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();

    public void OnGet()
    {
        Vehicules = context.Vehicules.Include(v => v.Model).Take(8).OrderByDescending(v => v.Id).ToList();
    }
}
