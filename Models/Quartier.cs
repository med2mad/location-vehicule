using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Quartier
{
    public int Id { get; set; }
    public string Nom { get; set; }

    [ForeignKey("Ville")]
    public int? VilleId { get; set; }
    public Ville? Ville { get; set; }
}
