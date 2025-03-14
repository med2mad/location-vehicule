using System.ComponentModel.DataAnnotations;

namespace RPtest.Models;

public class Conducteur
{
    public int Id { get; set; }
    [Required] public string Nom { get; set; } = string.Empty;
    [Required] public string Tel { get; set; } = string.Empty;
    [Required] public string CIN { get; set; } = string.Empty;

    public Location? Location { get; set; }
}
