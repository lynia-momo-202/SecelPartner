using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Core.Entities
{
    [Table("Avantage")]
    public class Avantage
    {
        #region proprietes
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        public string? Description { get; set; }
        #endregion
        #region relation
        [Required]
        [Display(Name = "Partenariat")]
        public string? PartenariatId { get; set; }
        public virtual Partenariat? Partenariat { get; set; }
        #endregion
    }
}
