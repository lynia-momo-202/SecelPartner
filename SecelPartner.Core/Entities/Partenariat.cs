using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Core.Entities
{
    [Table("Partenariat")]
    public class Partenariat
    {
        #region proprietes
        [Key]
        public string? Id { get; set; }
        [Required]
        [MinLength(15)]
        public string? Description { get; set; }
        #endregion
        #region relations
        [Display(Name = "Niveau Partenariat")]
        public int NiveauPartenariatId { get; set; }
        [Required]
        [Display(Name = "Type Partenariat")]
        public int TypePartenariatId { get; set; }
        public virtual NiveauPartenariat? NiveauPartenariat { get; set; }
        public virtual TypePartenariat? TypePartenariat { get; set; }
        public virtual IEnumerable<Condition>? Conditions { get; set; }
        public virtual IEnumerable<Avantage>? Avantages { get; set; }
        public virtual IEnumerable<ConditionRenouv>? ConditionRenouvs { get; set; }
        public virtual IEnumerable<ContratPartenariat>? ContratPartenariats { get; set; }
        #endregion
    }
}
