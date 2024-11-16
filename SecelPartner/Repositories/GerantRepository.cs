using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;
using SecelPartner.UI.Models;

namespace SecelPartner.UI.Repositories
{
    public class GerantRepository : IGerantRepository
    {
        private readonly SecelPartnerUIContext _context;

        public GerantRepository(SecelPartnerUIContext context)
        {
            _context = context;
        }

        public async Task Add(Gerant gerant)
        {
            await _context.Gerants.AddAsync(gerant);
            await Save();
        }

        public async Task<IEnumerable<Gerant>> Find(Expression<Func<Gerant, bool>> expression)
        {
            return _context.Gerants.Where(expression);
        }

        public async Task<IEnumerable<Gerant>> GetAll()
        {
            var all = _context.Gerants.Include(i => i.ContratPartenariat).Include(i => i.User);
            return all.ToList();
        }

        public async Task<Gerant> GetById(int id)
        {
            return await _context.Gerants.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var g = await GetById(id);
            if (g != null)
            {
                _context.Gerants.Remove(g);
            }
            await Save();
        }

        public List<Gerant> ListItems()
        {
            return _context.Gerants.ToList();
        }

        public List<Gerant> ListGerant(int id)
        {
            List<Gerant>? iduser = new List<Gerant>();
            foreach (var g in ListItems())
            {
                if (g.ContratPartenariatId == id)
                {
                    iduser.Add(g);
                }
            }
            return iduser;
        }

        public async Task Update(Gerant gerant)
        {
            var g = await GetById(gerant.Id);
            if (g != null)
            {
                g.ContratPartenariatId = gerant.ContratPartenariatId;
                g.UserId = gerant.UserId;
                g.Date_debut = gerant.Date_debut;
            }
            await Save();
        }

        public List<ContratPartenariat> ListContratGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats
        )
        {
            List<Gerant> gerants;
            List<ContratPartenariat>? gerantsContrat = new List<ContratPartenariat>();
            gerants = ListItems();

            foreach (var item in gerants)
            {
                if (item.UserId == id)
                {
                    foreach (var c in contrats)
                    {
                        if (item.ContratPartenariatId == c.Id)
                        {
                            gerantsContrat.Add(c);
                        }
                    }
                }
            }
            return gerantsContrat;
        }

        public List<Partenaire> ListPartenaireGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats,
            IEnumerable<Partenaire> partenaires
        )
        {
            List<ContratPartenariat>? contratPartenariats = ListContratGerant(id, contrats);
            List<Partenaire>? partenaire = new List<Partenaire>();
            foreach (var p in partenaires)
            {
                foreach (var c in contratPartenariats)
                {
                    if (p.Id == c.PartenaireId)
                    {
                        partenaire.Add(p);
                    }
                }
            }
            return partenaire;
        }

        public List<Partenariat> ListPartenariatGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats,
            IEnumerable<Partenariat> partenariats
        )
        {
            List<ContratPartenariat>? contratPartenariats = ListContratGerant(id, contrats);
            List<Partenariat>? partenariat = new List<Partenariat>();
            foreach (var p in partenariats)
            {
                foreach (var c in contratPartenariats)
                {
                    if (p.Id == c.PartenariatId)
                    {
                        partenariat.Add(p);
                    }
                }
            }
            return partenariat;
        }

        public List<Avantage> ListAvantageGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats,
            IEnumerable<Avantage> avantages
        )
        {
            List<ContratPartenariat>? contratPartenariats = ListContratGerant(id, contrats);
            List<Avantage>? avantage = new List<Avantage>();
            foreach (var p in avantages)
            {
                foreach (var c in contratPartenariats)
                {
                    if (p.PartenariatId == c.PartenariatId)
                    {
                        avantage.Add(p);
                    }
                }
            }
            return avantage;
        }

        public List<Condition> ListConditionGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats,
            IEnumerable<Condition> conditions
        )
        {
            List<ContratPartenariat>? contratPartenariats = ListContratGerant(id, contrats);
            List<Condition>? condition = new List<Condition>();
            foreach (var p in conditions)
            {
                foreach (var c in contratPartenariats)
                {
                    if (p.PartenariatId == c.PartenariatId)
                    {
                        condition.Add(p);
                    }
                }
            }
            return condition;
        }

        public List<ConditionRenouv> ListConditionRenouvGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats,
            IEnumerable<ConditionRenouv> conditionsRenouv
        )
        {
            List<ContratPartenariat>? contratPartenariats = ListContratGerant(id, contrats);
            List<ConditionRenouv>? conditionRenouv = new List<ConditionRenouv>();
            foreach (var p in conditionsRenouv)
            {
                foreach (var c in contratPartenariats)
                {
                    if (p.PartenariatId == c.PartenariatId)
                    {
                        conditionRenouv.Add(p);
                    }
                }
            }
            return conditionRenouv;
        }

        public List<Contact> ListContactGerant(
            string? id,
            IEnumerable<ContratPartenariat> contrats,
            IEnumerable<Contact> contacts
        )
        {
            List<ContratPartenariat>? contratPartenariats = ListContratGerant(id, contrats);
            List<Contact>? contact = new List<Contact>();
            foreach (var p in contacts)
            {
                foreach (var c in contratPartenariats)
                {
                    if (p.PartenaireId == c.PartenaireId)
                    {
                        contact.Add(p);
                    }
                }
            }
            return contact;
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
