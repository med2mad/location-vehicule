using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
[Authorize]
public class RevenusModel(ApplicationDbContext _context) : PageModel
{
    public IList<VehiculeRevenu> VehiculesAvecRevenu { get; set; }

    [BindProperty(SupportsGet = true)] public DateTime? StartDate { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? EndDate { get; set; }
    [BindProperty(SupportsGet = true)] public string Immatriculation { get; set; }
    [BindProperty(SupportsGet = true)] public string Sort { get; set; }
    
    public class VehiculeRevenu
    {
        public int VehiculeId { get; set; }
        public string Model { get; set; }
        public string Immatriculation { get; set; }
        public decimal Prix { get; set; }
        public DateTime Date { get; set; }
        public string Photo { get; set; }
        public decimal RevenuTotal { get; set; }
        public int NombreLocations { get; set; }
    }

    public async Task OnGetAsync()
    {
        var today = DateTime.Today;
        DateTime sd = new DateTime(today.Year, 1, 1);
        DateTime ed = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
        ed = ed.Date.AddDays(1).AddTicks(-1); //Add a day to include the entire end date (time = 00:00:00)

        if (StartDate.HasValue)
        {
            sd = StartDate.Value;
        }
        else
        {
            sd = DateTime.MinValue;
        }
        if (EndDate.HasValue)
        {
            ed = EndDate.Value.AddDays(1).AddTicks(-1); //Add a day to include the entire end date (time = 00:00:00)
        }
        else
        {
            ed = DateTime.MaxValue;
        }

        var query = _context.Vehicules
           .Select(v => new VehiculeRevenu
           {
               VehiculeId = v.Id,
               Immatriculation = v.Immatriculation,
               Photo = v.Photo,
               Prix = v.Prix,
               Date = v.Locations.FirstOrDefault().Date,
               Model = v.Model.Marque + ", " + v.Model.Nom,
               NombreLocations = _context.Locations.Count(l => l.VehiculeId == v.Id && l.Date >= sd && l.Date < ed),
               RevenuTotal = _context.Locations
                   .Where(l => l.VehiculeId == v.Id && l.Date >= sd && l.Date < ed)
                   .Join(_context.Paiements,
                       location => location.Id,
                       paiement => paiement.LocationId,
                       (location, paiement) => paiement.Montant)
                   .Sum()
           });
        
        if (!string.IsNullOrEmpty(Immatriculation))
        {
            query = query.Where(v => v.Immatriculation.Contains(Immatriculation));
        }

        if (!string.IsNullOrEmpty(Sort))
        {
            if (Sort == "Revenu")
            {
                query = query.OrderByDescending(r => r.RevenuTotal);
            }
            else if (Sort == "Locations")
            {
                query = query.OrderByDescending(r => r.NombreLocations);
            }
        }
        else
        {
            query = query.OrderByDescending(r => r.VehiculeId);
        }

        VehiculesAvecRevenu = await query.ToListAsync();
    }
}
