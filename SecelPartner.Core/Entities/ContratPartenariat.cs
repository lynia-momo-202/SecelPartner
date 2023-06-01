using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Core.Entities
{
    [Table("ContratPartenariat")]
    public class ContratPartenariat
    {
        #region proprietes
        [Key]
        public int Id { get; set; }
        [Display(Name = "Date de Signature")]
        public DateTime DateSign { get; set; }
        [Display(Name = "date d'Expiration")]
        public DateTime DateExpiration { get; set; }
        [Required]
       // [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Currency)]
        public int Montant { get; set; }
        [Required]
        [MinLength(10)]
        public string? Titre { get; set; }
        #endregion
        #region relation
        [Required]
        [Display(Name = "Partenariat")]
        public string? PartenariatId { get; set; }
        [Required]
        [Display(Name = "Partenaire")]
        public int PartenaireId { get; set; }
        public virtual Partenaire? Partenaire { get; set; }
        public virtual Partenariat? Partenariat { get; set; }
        #endregion
    }
}
