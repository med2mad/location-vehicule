using System.ComponentModel.DataAnnotations.Schema;

namespace RPtest.Models;

public class Quartier
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Ville { get; set; }
}
