using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;
using RPtest.Models;

namespace RPtest.Pages;
public class PaymentModel(ApplicationDbContext context) : PageModel
{
	public Vehicule Vehicule { get; set; } = new Vehicule { Immatriculation = "" };
	[BindProperty(SupportsGet = true)]
	public int VehiculeId { get; set; }
	[BindProperty(SupportsGet = true)]
	public Location Location { get; set; }

	public void OnGet()
	{
		Vehicule = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VehiculeId);
	}

	public IActionResult OnPost(int z)
	{
		Location.VehiculeId = z;
		context.Locations.Add(Location);
		context.SaveChanges();

		return RedirectToPage("Reserved");
	}
}
