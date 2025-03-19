using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class ListingModel(ApplicationDbContext context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();

    public void OnGet(string? model, string? type, string? couleur)
    {
        var query = context.Vehicules.Include(v => v.Model).AsQueryable();
        if (!string.IsNullOrEmpty(model))
        {
            query = query.Where(v => v.Model.Description == model);
        }
        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(v => v.Model.Type == type);
        }
        if (!string.IsNullOrEmpty(couleur))
        {
            query = query.Where(v => v.Couleur == couleur);
        }
        Vehicules = query.ToList();
    }
}
