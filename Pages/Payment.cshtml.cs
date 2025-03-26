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
    [BindProperty]
    public Paiement Paiement { get; set; } = new();

    [BindProperty(SupportsGet = true)] public string villeDepart { get; set; }
    [BindProperty(SupportsGet = true)] public string quartierDepart { get; set; } = string.Empty;
    [BindProperty(SupportsGet = true)] public string villeRetour { get; set; }
    [BindProperty(SupportsGet = true)] public string quartierRetour { get; set; } = string.Empty;

    public void OnGet()
    {
        Vehicule = context.Vehicules.Include(v => v.Model).FirstOrDefault(v => v.Id == VehiculeId);
        Paiement.Montant = Vehicule.Prix;
    }

    public IActionResult OnPost(int VId)
    {
        var now = DateTime.Now;

        Location.VehiculeId = VId;
        Location.Date = now;
        Location.LieuDepart = villeDepart + " " + quartierDepart;
        Location.LieuRetour = villeRetour + " " + quartierRetour;
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
        message += " - Numéro de Location : #" + LId.ToString("D6") + "\n";
        message += " - Commande effectuée le : " + now.ToString("dd-MM-yyyy") + "\n";
        message += " - Véhicule : " + v.Model.Marque + " " + v.Model.Nom + "\n";
        message += " - Réservé pour : " + Location.DateDepart.ToString("dd-MM-yyyy") + "\n";
        message += " - Montant : " + Paiement.Montant + " DH\n";
        message += "\n";
        message += "Si vous avez la moindre question, n’hésitez pas à nous contacter.";

        var x = new EmailSender();
        x.SendEmailAsync(Paiement.Email, "Réservation de véhicule", message);

        return RedirectToPage("Reserved");
    }
}
