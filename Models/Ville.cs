namespace RPtest.Models;

public class Ville
{
    public int Id { get; set; }
    public string Nom { get; set; }

    public ICollection<Quartier>? Quartiers { get; set; }
}
