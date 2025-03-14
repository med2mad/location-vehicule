using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Location
{
    public int Id { get; set; }
    public string NomClient { get; set; }
    public string Tel { get; set; }
    public string? Email { get; set; }
    public string PlaceDebut { get; set; }
    public string PlaceFin { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public string Tarif { get; set; } = "jours"; //jours;heurs;Kilométrage
    public string Status { get; set; } = "Reservé"; //Reservé;Complet;Annulé

    [ForeignKey("Vehicule")]
    public int? VehiculeId { get; set; }
    public Vehicule? Vehicule { get; set; }

    [ForeignKey("Conducteur")]
    public int? ConducteurId { get; set; }
    public Conducteur? Conducteur { get; set; }
}
