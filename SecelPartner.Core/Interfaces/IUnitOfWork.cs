
namespace SecelPartner.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IPartenaireRepository Partenaires { get; }
        IAvantageRepository Avantages { get; }
        IConditionRepository Conditions { get; }
        IConditionRenouvRepository ConditionsRenouv { get; }
        IContactRepository Contacts { get; }
        IContratPartenariatRepository Contrats { get; }
        INiveauPartenariatRepository NiveauxPartenariat { get; }
        ITypePartenariatRepository TypesPartenariat { get; }
        IPartenariatRepository Partenariats { get; }
        ISendEmailRepository Emails { get; }
        int Complete();
    }
}
