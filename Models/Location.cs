using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Location
{
	public int Id { get; set; }
	public string NomClient { get; set; }
	public string Tel { get; set; }
	public string PlaceDebut { get; set; }
	public string PlaceFin { get; set; }
	public DateTime DateDebut { get; set; }
	public DateTime DateFin { get; set; }
	public string Tarif { get; set; } = "Jour"; //Jour;Heure;Kilomètre
	public string Rib { get; set; }
	//[RegularExpression(@"^\d{3}$", ErrorMessage = "Entrez 3 chiffres")]
	public string CVC { get; set; }
	//[RegularExpression(@"^\d{2}/\d{2}$", ErrorMessage = "Mois non valide")]
	public string Expiration { get; set; }
	public string Email { get; set; }
	public string Status { get; set; } = "Reservé"; //Reservé;Complet;Annulé

	[ForeignKey("Vehicule")]
	public int? VehiculeId { get; set; }
	public Vehicule? Vehicule { get; set; }

	[ForeignKey("Conducteur")]
	public int? ConducteurId { get; set; }
	public Conducteur? Conducteur { get; set; }
}
