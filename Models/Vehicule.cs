using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace RPtest.Models;

public class Vehicule
{
    public int Id { get; set; }
    public string? Immatriculation { get; set; } = string.Empty;
    public string? Couleur { get; set; } = string.Empty;
    public string? Carburant { get; set; } = string.Empty; //Essence;Diesel
    public string? Boite { get; set; } = string.Empty; //Manuelle;Automatique
    public string? Climatisation { get; set; } = string.Empty; //Avec;Sans
    public string? Photo { get; set; } = string.Empty;
    public decimal Prix { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int? KilometrageActuel { get; set; }
    public int? KilometrageEntreVidanges { get; set; }

    [ForeignKey("Model")]
    public int? ModelId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public Model? Model { get; set; }

    public ICollection<Location>? Locations { get; set; }
    public ICollection<Depense>? Depenses { get; set; }
    public ICollection<Vidange>? Vidanges { get; set; } //1 ans + input KilometrageEntreVidanges for each vehicule
    public ICollection<VisiteTechnique>? VisitesTechniques { get; set; } //1 ans la premiere foi et 6 mois apres pour location (5/1 pour normal)
}
