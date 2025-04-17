using System.ComponentModel.DataAnnotations;

namespace RPtest.Models;

public class Model
{
    public int Id { get; set; }
    [Required] public string Nom { get; set; }
    [Required] public string Marque { get; set; }
    public string Type { get; set; } //Voiture;Luxe;Camion;Camionnette;Camping;Mini Bus;
    public int Passagers { get; set; } = 0;
    public decimal Bagage { get; set; } = 0;
    
    public ICollection<Vehicule>? Vehicules { get; set; }
}
