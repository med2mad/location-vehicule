using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using System.Text;
using System.Text.Json;

namespace RPtest.Pages;

public class AddCarModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty]
    public Vehicule Vehicule { get; set; }
    [BindProperty]
    public Model Model { get; set; }

    public List<string> Marques { get; set; } = new();
    public List<Model> Marques_Models { get; set; } = new();
    public string ModelsJ { get; set; } = "[]";

    public void OnGet()
    {
        Marques = _context.Models.Select(m => m.Marque).Distinct().ToList();
        Marques_Models = _context.Models.Select(m => new Model { Marque = m.Marque, Nom = m.Nom }).OrderBy(m => m.Marque).ToList();
        ModelsJ = JsonSerializer.Serialize(Marques_Models);
    }

    public IActionResult OnPost()
    {
        var x = _context.Models.Select(m => new Model { Id = m.Id, Marque = m.Marque, Nom = m.Nom }).FirstOrDefault(m => m.Marque == Model.Marque && m.Nom == Model.Nom);
        int id = 0;

        if (x == null)
        {
            Model m = new Model { Marque = Model.Marque, Nom = Model.Nom };
            _context.Models.Add(m);
            _context.SaveChanges();
            id = m.Id;
        }
        else
        {
            id = x.Id;
        }

        _context.Vehicules.Add(new Vehicule { ModelId = id, Prix = Vehicule.Prix, Immatriculation = Vehicule.Immatriculation });
        _context.SaveChanges();

        TempData["message"] = "Véhicule Ajouté!";
        return RedirectToPage();
    }
}