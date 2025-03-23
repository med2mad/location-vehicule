using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace RPtest.Models;

public class Vehicule
{
    public int Id { get; set; }
    [Required] public required string Immatriculation { get; set; }
    public string Couleur { get; set; } = string.Empty;
    public string Carburant { get; set; } = string.Empty; //Essence;Diesel 
    public bool? Climatisation { get; set; } = null;
    public string Photo { get; set; } = string.Empty;
    public decimal Prix { get; set; }

    [ForeignKey("Model")]
    public int? ModelId { get; set; }
    public Model? Model { get; set; }

    public ICollection<Location>? Locations { get; set; }
}
