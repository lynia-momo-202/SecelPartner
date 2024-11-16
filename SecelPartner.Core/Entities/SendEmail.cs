using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecelPartner.Core.Entities
{
    [Table("Email")]
    public class SendEmail
    {
        #region proprietes
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        public int Tel { get; set; }

        /// <summary>
        /// email de celui a qui l'email est destine
        /// </summary>
        [Required]
        [EmailAddress]
        public string? ToEmail { get; set; }

        /// <summary>
        /// email de celui envoie l'email
        /// </summary>
        [Required]
        [EmailAddress]
        public string? FromEmail { get; set; }

        /// <summary>
        /// le sujet : l'objet de lemail
        /// </summary>
        [Required]
        public string? Subject { get; set; }

        /// <summary>
        /// le message proprement dit
        /// </summary>
        [Required]
        public string? Message { get; set; }
        #endregion
    }
}
