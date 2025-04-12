namespace RPtest.Models;

public class Notification
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string? Description { get; set; }
    public int? Jours { get; set; } = null;
    public int? Mois { get; set; } = null;
    public int? Annees { get; set; } = null;

    public ICollection<Depense>? Depenses { get; set; }
}
