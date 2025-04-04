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
    public Location Location { get; set; } //just for dd/MM/yyyy format

    public class VehiculeRevenu
    {
        public int Id { get; set; }
        public int VehiculeId { get; set; }
        public string Model { get; set; }
        public string Immatriculation { get; set; }
        public decimal Prix { get; set; }
        public string Photo { get; set; }
        public decimal RevenuTotal { get; set; }
        public int NombreLocations { get; set; }
    }

    public async Task OnGetAsync()
    {
        VehiculesAvecRevenu = await _context.Vehicules
            .Select(v => new VehiculeRevenu
            {
                Id = v.Id,
                Immatriculation = v.Immatriculation,
                VehiculeId = v.Id,
                Photo = v.Photo,
                Prix = v.Prix,
                Model = v.Model.Marque + ", " + v.Model.Nom,
                NombreLocations = _context.Locations.Where(l => l.VehiculeId == v.Id).Count(),
                RevenuTotal = _context.Locations
                    .Where(l => l.VehiculeId == v.Id)
                    .Join(_context.Paiements,
                        location => location.Id,
                        paiement => paiement.LocationId,
                        (location, paiement) => paiement.Montant)
                    .Sum()
            }).OrderByDescending(l => l.VehiculeId)
            .ToListAsync();
    }
}
