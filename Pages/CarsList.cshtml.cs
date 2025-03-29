using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using System.Text.Json;

namespace RPtest.Pages;
public class CarsList(ApplicationDbContext _context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();
    public int cnt;

    [BindProperty(SupportsGet = true)] public Vehicule Vehicule { get; set; }
    [BindProperty(SupportsGet = true)] public Model Model { get; set; }

    public List<string> Marques { get; set; } = new();
    public List<Model> Marques_Models { get; set; } = new();
    public string ModelsJ { get; set; } = "[]";

    public IActionResult OnGet()
    {
        Marques = _context.Models.Select(m => m.Marque).Distinct().ToList();
        Marques_Models = _context.Models.Select(m => new Model { Marque = m.Marque, Nom = m.Nom }).OrderBy(m => m.Marque).ToList();
        ModelsJ = JsonSerializer.Serialize(Marques_Models);

        var query = _context.Vehicules.Include(v => v.Model).Include(v => v.Locations).AsQueryable();
        if (!string.IsNullOrEmpty(Model.Marque))
        {
            query = query.Where(v => v.Model.Marque == Model.Marque);
        }
        if (!string.IsNullOrEmpty(Model.Nom))
        {
            query = query.Where(v => v.Model.Nom == Model.Nom);
        }
        if (!string.IsNullOrEmpty(Model.Type))
        {
            query = query.Where(v => v.Model.Type == Model.Type);
        }
        if (!string.IsNullOrEmpty(Vehicule.Immatriculation))
        {
            query = query.Where(v => v.Immatriculation.Contains(Vehicule.Immatriculation));
        }

        Vehicules = query.OrderByDescending(l => l.Id).ToList();
        cnt = Vehicules.Count;

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var vehicule = await _context.Vehicules.FindAsync(id);
        if (vehicule == null)
        {
            return NotFound();
        }

        _context.Vehicules.Remove(vehicule);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}
