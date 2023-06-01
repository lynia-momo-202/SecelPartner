using SecelPartner.Core.Entities;
using SecelPartner.UI.Models;
using System.Linq.Expressions;

namespace SecelPartner.UI.Interfaces
{
    public interface IGerantRepository
    {
        Task<Gerant> GetById(int id);
        Task<IEnumerable<Gerant>> GetAll();
        List<Gerant> ListItems();
        List<Gerant> ListGerant(int id);
        Task<IEnumerable<Gerant>> Find(Expression<Func<Gerant, bool>> expression);
        Task Add(Gerant gerant);
        Task Delete(int id);
        List<ContratPartenariat> ListContratGerant(string? id , IEnumerable<ContratPartenariat> contrats);
        List<Partenaire> ListPartenaireGerant(string? id , IEnumerable<ContratPartenariat> contrats, IEnumerable<Partenaire> partenaires);
        List<Partenariat> ListPartenariatGerant(string? id , IEnumerable<ContratPartenariat> contrats, IEnumerable<Partenariat> partenariats);
        List<Avantage> ListAvantageGerant(string? id , IEnumerable<ContratPartenariat> contrats, IEnumerable<Avantage> avantages);
        List<Condition> ListConditionGerant(string? id , IEnumerable<ContratPartenariat> contrats, IEnumerable<Condition> conditions);
        List<ConditionRenouv> ListConditionRenouvGerant(string? id , IEnumerable<ContratPartenariat> contrats, IEnumerable<ConditionRenouv> conditionsRenouv);
        List<Contact> ListContactGerant(string? id , IEnumerable<ContratPartenariat> contrats, IEnumerable<Contact> contacts);
        Task Update(Gerant gerant);
    }
}
