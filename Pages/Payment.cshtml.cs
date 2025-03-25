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
    [BindProperty(SupportsGet = true)]
    public Paiement Paiement { get; set; }

    public void OnGet()
    {
        Vehicule = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VehiculeId);
    }

    public IActionResult OnPost(int VId)
    {
        var now = DateTime.Now;

        Location.VehiculeId = VId;
        Location.Date = now;
        context.Locations.Add(Location);
        context.SaveChanges();
        var LId = Location.Id;

        Paiement.LocationId = LId;
        Paiement.Date = now;
        context.Paiements.Add(Paiement);
        context.SaveChanges();

        var v = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VId);
        string message = "Cher(e),\"" + Location.NomClient + "\"\n";
        message += "Nous avons bien reçu votre réservation. Voici les détails de votre achat :\n";
        message += " - Numéro de réservation : #" + LId.ToString("D6") + "\n";
        message += " - Comande effectuée le : " + DateTime.Now.ToString("dd-MM-yyyy") + "\n";
        message += " - Véhicule : " + v.Model.Nom + "\n";
        message += " - Réservé pour : " + Location.DateDepart.ToString("dd-MM-yyyy") + "\n";
        message += " - Montant total : " + v.Prix + " DH\n";
        message += "\n";
        message += "Si vous avez la moindre question, n’hésitez pas à nous contacter.";

        var x = new EmailSender();
        x.SendEmailAsync(Paiement.Email, "Réservation de véhicule", message);

        return RedirectToPage("Reserved");
    }
}
