using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class ListingModel(ApplicationDbContext context) : PageModel
{
    public List<Vehicule> Vehicules { get; set; } = new List<Vehicule>();
    [BindProperty(SupportsGet = true)]
    public Location Location { get; set; }

    public void OnGet(string? model, string? type, string? couleur, string? marque)
    {
        var query = context.Vehicules.Include(v => v.Model).AsQueryable();
        if (!string.IsNullOrEmpty(marque))
        {
            query = query.Where(v => v.Model.Marque == marque);
        }
        //if (!string.IsNullOrEmpty(type))
        //{
        //    query = query.Where(v => v.Model.Type == type);
        //}
        //if (!string.IsNullOrEmpty(couleur))
        //{
        //    query = query.Where(v => v.Couleur == couleur);
        //}
        Vehicules = query.ToList();
    }
}
