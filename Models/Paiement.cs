using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Paiement
{
    public int Id { get; set; }
    public decimal Montant { get; set; }
    public string Rib { get; set; }
    //[RegularExpression(@"^\d{3}$", ErrorMessage = "Entrez 3 chiffres")]
    public string CVC { get; set; }
    //[RegularExpression(@"^\d{2}/\d{2}$", ErrorMessage = "Mois non valide")]
    public string Expiration { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("Location")]
    public int? LocationId { get; set; }
    public Location? Location { get; set; }
}
