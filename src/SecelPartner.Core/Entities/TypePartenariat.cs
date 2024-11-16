using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Core.Entities
{
    [Table("TypePartenariat")]
    public class TypePartenariat
    {
        #region proprietes
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string? Nom { get; set; }
        #endregion
        #region relation
        #endregion
    }
}
