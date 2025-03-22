using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class RentModel(ApplicationDbContext context) : PageModel
{
    public Vehicule Vehicule { get; set; } = new Vehicule { Immatriculation = "" };
    [BindProperty(SupportsGet = true)]
    public Location Location { get; set; } = new Location();

    public void OnGet(int VehiculeId)
    {
        if (Location.DateDepart.ToString("yyyy-MM-dd") == "0001-01-01") { Location.DateDepart = DateTime.Now; }
        if (Location.DateRetour.ToString("yyyy-MM-dd") == "0001-01-01") { Location.DateRetour = DateTime.Now; }
        Vehicule = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VehiculeId);
    }


}
