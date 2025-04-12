using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RPtest.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<RPtest.Models.Model> Models { get; set; } = default!;
    public DbSet<RPtest.Models.Vehicule> Vehicules { get; set; } = default!;
    public DbSet<RPtest.Models.Conducteur> Conducteurs { get; set; } = default!;
    public DbSet<RPtest.Models.Location> Locations { get; set; } = default!;
    public DbSet<RPtest.Models.Paiement> Paiements { get; set; } = default!;
    public DbSet<RPtest.Models.Quartier> Quartiers { get; set; } = default!;
    public DbSet<RPtest.Models.VisiteTechnique> VisitesTechniques { get; set; } = default!;
    public DbSet<RPtest.Models.Vidange> Vidanges { get; set; } = default!;
    public DbSet<RPtest.Models.Depense> Depenses { get; set; } = default!;
    public DbSet<RPtest.Models.Notification> Notifications { get; set; } = default!;
    public DbSet<RPtest.Models.TypeVehicule> Types { get; set; } = default!;
    public DbSet<RPtest.Models.Couleur> Couleurs { get; set; } = default!;

}
