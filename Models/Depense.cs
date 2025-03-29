using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Depense
{
    public int Id { get; set; }
    public string Designation { get; set; }
    public decimal Montant { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("Vehicule")]
    public int? VehiculeId { get; set; }
    public Vehicule? Vehicule { get; set; }
}
