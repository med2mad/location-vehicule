using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using System.Text.Json;

namespace RPtest.Pages;
public class RentModel(ApplicationDbContext _context) : PageModel
{
    public Vehicule Vehicule { get; set; } = new Vehicule { Immatriculation = "" };
    [BindProperty(SupportsGet = true)]
    public Location Location { get; set; } = new Location();

    public List<Ville> Villes { get; set; }
    public List<Quartier> Quartiers { get; set; }
    public string QuartiersJ { get; set; } = "[]";

    public void OnGet(int VehiculeId)
    {
        if (Location.DateDepart.ToString("yyyy-MM-dd") == "0001-01-01") { Location.DateDepart = DateTime.Now; }
        if (Location.DateRetour.ToString("yyyy-MM-dd") == "0001-01-01") { Location.DateRetour = DateTime.Now; }
        Vehicule = _context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VehiculeId);
        Villes = _context.Villes.ToList();
        //Quartiers = _context.Quartiers.ToList();
        Quartiers = _context.Quartiers.Include(q => q.Ville).Select(q => new Quartier { Nom = q.Nom, Ville = q.Ville }).OrderBy(q => q.Ville).ToList();
        QuartiersJ = JsonSerializer.Serialize(Quartiers);
    }

}
