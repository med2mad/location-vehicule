using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPtest.Data;

namespace RPtest.Pages;
public class CarsList(ApplicationDbContext _context) : PageModel
{
	public IActionResult OnGetGetCars()
	{
		var Vehicules = _context.Vehicules.Include(v => v.Model)
		.Select(v => new
		{
			photo = "/images/" + v.Photo,
			model = v.Model.Nom,
			immatriculation = v.Immatriculation,
			prix = v.Prix,
			climatisation = v.Climatisation,
			vidange = v.Date.ToString("dd-MM-yyyy"),
		}).ToList();

		return new JsonResult(new { data = Vehicules });
	}
}
