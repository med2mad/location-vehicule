using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class RentModel(ApplicationDbContext context) : PageModel
{
    public Vehicule Vehicule { get; set; } = new Vehicule { Immatriculation = "" };
    [BindProperty]
    public Location Location { get; set; }

    public void OnGet(int VehiculeId)
    {
        Vehicule = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VehiculeId);
    }

}
