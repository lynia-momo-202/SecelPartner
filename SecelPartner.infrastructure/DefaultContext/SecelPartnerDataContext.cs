using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;

namespace SecelPartner.Infrastructure.DefaultContext
{
    public class SecelPartnerDataContext : DbContext
    {
        #region constructeur
        public SecelPartnerDataContext(DbContextOptions<SecelPartnerDataContext> options)
            : base(options) { }
        #endregion
        #region dbset
        public DbSet<Partenaire>? Partenaires { get; set; }
        public DbSet<ContratPartenariat>? ContratPartenariats { get; set; }
        public DbSet<Avantage>? Avantages { get; set; }
        public DbSet<Condition>? Conditions { get; set; }
        public DbSet<TypePartenariat>? TypePartenariats { get; set; }
        public DbSet<NiveauPartenariat>? NiveauPartenariats { get; set; }
        public DbSet<SendEmail>? SendEmails { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Partenariat>? Partenariats { get; set; }
        public DbSet<ConditionRenouv>? ConditionRenouvs { get; set; }
        #endregion
        #region relations
        #endregion
    }
}
