using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class ReservedModel(ApplicationDbContext context) : PageModel
{
    [BindProperty]
    public Vehicule Vehicule { get; set; } = new Vehicule { Immatriculation = "" };
    [BindProperty]
    public Location Location { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        context.Locations.Add(Location);
        context.SaveChanges();
    }
}

