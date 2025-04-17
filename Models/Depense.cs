using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Depense
{
    public int Id { get; set; }
    public decimal Montant { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("Vehicule")]
    public int? VehiculeId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public Vehicule? Vehicule { get; set; }

    [ForeignKey("Notification")]
    public int? NotificationId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public Notification? Notification { get; set; }
}
