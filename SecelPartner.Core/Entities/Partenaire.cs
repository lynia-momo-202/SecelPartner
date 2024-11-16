using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace SecelPartner.Core.Entities
{
    [Table("Partenaire")]
    public class Partenaire
    {
        #region proprietes
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Nom { get; set; }
        public string? Adresse { get; set; }

        [Required]
        [Display(Name = "numero")]
        public int NumTel { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Siteweb { get; set; }

        [Display(Name = "Secteur d'Activite")]
        public string? SecteurAc { get; set; }
        public string? Statut { get; set; }
        #endregion
        #region relations
        public string? LogoName { get; set; }
        public string? LogoPath { get; set; }

        [NotMapped]
        public IFormFile? Logo { get; set; }
        public virtual IEnumerable<Contact>? Contacts { get; set; }
        public virtual IEnumerable<ContratPartenariat>? ContratPartenariats { get; set; }
        #endregion
    }
}
