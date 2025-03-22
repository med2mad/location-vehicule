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
        var v = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == z);

        string message = "Cher(e),\"" + Location.NomClient + "\"\n";
        message += "Nous avons bien reçu votre réservation. Voici les détails de votre achat :\n";
        message += " - Numéro de réservation : #35647\n";
        message += " - Comande effectuée le : " + DateTime.Now.ToString("dd-MM-yyyy") + "\n";
        message += " - Véhicule : " + v.Model.Nom + "\n";
        message += " - Réservé pour : " + Location.DateDepart.ToString("dd-MM-yyyy") + "\n";
        message += " - Montant total : " + v.Prix + " DH\n";
        message += "\n";
        message += "Si vous avez la moindre question, n’hésitez pas à nous contacter.";

        var x = new EmailSender();
        x.SendEmailAsync(Location.Email, "Réservation de véhicule", message);

        Location.VehiculeId = z;
        context.Locations.Add(Location);
        context.SaveChanges();

        return RedirectToPage("Reserved");
    }
}
