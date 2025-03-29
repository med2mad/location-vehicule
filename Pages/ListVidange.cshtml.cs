using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;

public class ListVidangeModel(ApplicationDbContext _context) : PageModel
{
	public IList<Vidange> Vidanges { get; set; }
	public int VehiculeId { get; set; }
	public string VehiculeMarque { get; set; }
	public string VehiculeModel { get; set; }
	public string VehiculeImmatriculation { get; set; }

	public async Task OnGetAsync(int vehiculeId)
	{
		VehiculeId = vehiculeId;

		var vehicule = await _context.Vehicules.Include(v => v.Model)
			.FirstOrDefaultAsync(v => v.Id == vehiculeId);

		if (vehicule != null)
		{
			VehiculeMarque = vehicule.Model.Marque;
			VehiculeModel = vehicule.Model.Nom;
			VehiculeImmatriculation = vehicule.Immatriculation;
		}

		Vidanges = await _context.Vidanges
			.Where(d => d.VehiculeId == vehiculeId)
			.OrderByDescending(d => d.Id)
			.ToListAsync();
	}
}
