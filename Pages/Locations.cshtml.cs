using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RPtest.Pages;
public class LocationsModel(ApplicationDbContext context) : PageModel
{
    public List<Location> Locations { get; set; } = new List<Location>();

    public void OnGet()
    {
        Locations = context.Locations.Include(l => l.Vehicule).OrderByDescending(l => l.Id).ToList();
    }
}

