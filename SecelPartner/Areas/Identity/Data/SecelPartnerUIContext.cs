using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Models;
using SecelPartner.Core.Entities;

namespace SecelPartner.UI.Data;

public class SecelPartnerUIContext : IdentityDbContext<SecelPartnerUIUser>
{
    public SecelPartnerUIContext(DbContextOptions<SecelPartnerUIContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Gerant> Gerants { get; set; }
    public DbSet<SecelPartnerUIUser> SecelPartnerUIUsers { get; set; }
    public DbSet<SecelPartner.Core.Entities.ContratPartenariat>? ContratPartenariat { get; set; }
    public DbSet<SecelPartner.Core.Entities.Partenaire>? Partenaire { get; set; }
}
