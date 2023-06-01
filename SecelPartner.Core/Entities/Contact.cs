using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecelPartner.Core.Entities
{
    [Table("Contact")]

    public class Contact
    {
        #region proprietes
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        [Required]
        public long Tel { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(3)]
        public string? Poste { get; set; }
        public string? PhotoName { get; set; }
        public string? PhotoPath { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        #endregion
        #region Relations
        [Required]
        [Display(Name = "Partenaire")]
        public int PartenaireId { get; set; }
        
        public virtual Partenaire? Partenaire { get; set; }
        #endregion
    }
}
