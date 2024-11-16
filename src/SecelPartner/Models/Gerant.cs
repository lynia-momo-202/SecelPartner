using System.ComponentModel.DataAnnotations;
using SecelPartner.Core.Entities;
using SecelPartner.UI.Areas.Identity.Data;

namespace SecelPartner.UI.Models
{
    public class Gerant
    {
        #region proprietes
        [Key]
        public int Id { get; set; }
        public DateTime Date_debut { get; set; }
        #endregion
        #region relation
        [Required]
        [Display(Name = "Contrat Partenariat")]
        public int ContratPartenariatId { get; set; }

        [Required]
        [Display(Name = "Administrateur Partenariat")]
        public string? UserId { get; set; }
        public virtual SecelPartnerUIUser? User { get; set; }
        public virtual ContratPartenariat? ContratPartenariat { get; set; }
        #endregion
    }
}
