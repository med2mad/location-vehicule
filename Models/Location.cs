using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Location
{
    public int Id { get; set; }
    public string NomClient { get; set; }
    public string Tel { get; set; }
    public string LieuDepart { get; set; }
    public string LieuRetour { get; set; }
    public DateTime Date { get; set; }
    public DateTime DateDepart { get; set; }
    public DateTime DateRetour { get; set; }
    public string Tarif { get; set; } = "Jour"; //Jour;Heure;Kilomètre
    public string Statut { get; set; } = "Réservé"; //Réservé;Complet;Annulé

    [ForeignKey("Vehicule")]
    public int? VehiculeId { get; set; }
    public Vehicule? Vehicule { get; set; }

    [ForeignKey("Conducteur")]
    public int? ConducteurId { get; set; }
    public Conducteur? Conducteur { get; set; }

    public ICollection<Paiement>? Paiement { get; set; }
}
