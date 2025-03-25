using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RPtest.Data;
using RPtest.Models;
using System.Text.Json;

namespace RPtest.Pages;
public class ListingModel(ApplicationDbContext _context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();
    [BindProperty(SupportsGet = true)] public Location Location { get; set; }

    public List<string> Marques { get; set; } = new();
    public List<Model> Marques_Models { get; set; } = new();
    public string ModelsJ { get; set; } = "[]";
    public int? min { get; set; } = null;
    public int? max { get; set; } = null;

    [BindProperty(SupportsGet = true)] public Vehicule Vehicule { get; set; }
    [BindProperty(SupportsGet = true)] public Model Model { get; set; }
    [BindProperty(SupportsGet = true)] public int? PrixMin { get; set; } = null;
    [BindProperty(SupportsGet = true)] public int? PrixMax { get; set; } = null;
    [BindProperty(SupportsGet = true)] public string Climatisation { get; set; }

    public IActionResult OnGet()
    {
        Marques = _context.Models.Select(m => m.Marque).Distinct().ToList();
        Marques_Models = _context.Models.Select(m => new Model { Marque = m.Marque, Nom = m.Nom }).OrderBy(m => m.Marque).ToList();
        ModelsJ = JsonSerializer.Serialize(Marques_Models);
        var x = _context.Vehicules.Select(v => v.Prix).AsQueryable();
        min = (int)Math.Floor(x.Min()); if (PrixMin == null) PrixMin = min;
        max = (int)Math.Ceiling(x.Max()); if (PrixMax == null) PrixMax = max;

        var query = _context.Vehicules.Include(v => v.Model).AsQueryable();
        if (!string.IsNullOrEmpty(Model.Marque))
        {
            query = query.Where(v => v.Model.Marque == Model.Marque);
        }
        if (!string.IsNullOrEmpty(Model.Nom))
        {
            query = query.Where(v => v.Model.Nom == Model.Nom);
        }
        if (!string.IsNullOrEmpty(Climatisation))
        {
            query = query.Where(v => v.Climatisation == (Climatisation == "Avec"));
        }
        if (!string.IsNullOrEmpty(Model.Type))
        {
            query = query.Where(v => v.Model.Type == Model.Type);
        }
        if (!string.IsNullOrEmpty(Vehicule.Couleur))
        {
            query = query.Where(v => v.Couleur == Vehicule.Couleur);
        }
        if (PrixMin > 0)
        {
            query = query.Where(v => v.Prix >= PrixMin);
        }
        if (PrixMax > 0)
        {
            query = query.Where(v => v.Prix <= PrixMax);
        }
        Vehicules = query.ToList();

        return Page();
    }
}
