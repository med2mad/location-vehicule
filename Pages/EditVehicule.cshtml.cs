using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using System.Text.Json;

namespace RPtest.Pages;
public class EditVehiculeModel(ApplicationDbContext _context) : PageModel
{
    [BindProperty] public Vehicule Vehicule { get; set; }
    [BindProperty] public Model Model { get; set; } = new();
    [BindProperty] public string Climatisation { get; set; }
    [BindProperty] public string Photo { get; set; }

    public List<string> Marques { get; set; } = new();
    public List<Model> Marques_Models { get; set; } = new();
    public string ModelsJ { get; set; } = "[]";

    public bool IsNew => Vehicule == null || Vehicule.Id == 0;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Marques = _context.Models.Select(m => m.Marque).Distinct().ToList();
        Marques_Models = _context.Models.Select(m => new Model { Marque = m.Marque, Nom = m.Nom }).OrderBy(m => m.Marque).ToList();
        ModelsJ = JsonSerializer.Serialize(Marques_Models);

        if (id == null)
        {
            Vehicule = new Vehicule();
        }
        else
        {
            Vehicule = await _context.Vehicules.Include(v => v.Model).FirstOrDefaultAsync(v => v.Id == id);
            if (Vehicule == null)
            {
                return NotFound();
            }
            Model = Vehicule.Model;
            Climatisation = Vehicule.Climatisation == true ? "Avec" : "Sans";
            Photo = Vehicule.Photo;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile imageFile)
    {
        var x = _context.Models.Select(m => new Model { Id = m.Id, Marque = m.Marque, Nom = m.Nom }).FirstOrDefault(m => m.Marque == Model.Marque && m.Nom == Model.Nom);
        Vehicule.ModelId = x.Id;

        Vehicule.Climatisation = Climatisation == "Avec";

        if (imageFile != null)
        {
            string fileName = Path.GetFileName(imageFile.FileName);
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            string filePath = Path.Combine(uploadsFolder, Guid.NewGuid().ToString() + "_" + fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            Vehicule.Photo = fileName;
        }
        else
        {
            Vehicule.Photo = Photo;
        }

        if (Vehicule.Id == 0)
        {
            _context.Vehicules.Add(Vehicule);
        }
        else
        {
            _context.Vehicules.Update(Vehicule);
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("CarsList");
    }
}
