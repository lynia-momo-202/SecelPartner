using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly SecelPartnerDataContext _context;

        public UnitOfWork(SecelPartnerDataContext context)
        {
            _context = context;
            Partenaires = new PartenaireRepository(_context);
            Avantages = new AvantageRepository(_context);
            Conditions = new ConditionRepository(_context);
            ConditionsRenouv = new ConditionRenouvRepository(_context);
            Contacts = new ContactRepository(_context);
            Contrats = new ContratPartenariatRepository(_context);
            NiveauxPartenariat = new NiveauPartenariatRepository(_context);
            TypesPartenariat = new TypePartenariatRepository(_context);
            Partenariats = new PartenariatRepository(_context);
            Emails = new SendEmailRepository(_context);
        }

        public IPartenaireRepository Partenaires { get; private set; }

        public IAvantageRepository Avantages { get; private set; }

        public IConditionRepository Conditions { get; private set; }

        public IConditionRenouvRepository ConditionsRenouv { get; private set; }

        public IContactRepository Contacts { get; private set; }

        public IContratPartenariatRepository Contrats { get; private set; }

        public INiveauPartenariatRepository NiveauxPartenariat { get; private set; }

        public ITypePartenariatRepository TypesPartenariat { get; private set; }
        public IPartenariatRepository Partenariats { get; private set; }

        public ISendEmailRepository Emails { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
