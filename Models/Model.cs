using System.ComponentModel.DataAnnotations;

namespace RPtest.Models;

public class Model
{
    public int Id { get; set; }
    [Required] public string Nom { get; set; }
    [Required] public string Type { get; set; } = "Voiture"; //Voiture;Luxe;Camion;Camionnette;Camping;Moto
    public string Carburant { get; set; } = string.Empty; //Essence;Diesel 
    public string Description { get; set; } = string.Empty;
    public int Passagers { get; set; } = 0;
    public decimal Bagage { get; set; } = 0;
    public bool? Climatise { get; set; } = null;

    public ICollection<Vehicule>? Vehicules { get; set; }
}