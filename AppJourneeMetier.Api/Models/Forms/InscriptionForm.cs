using System.ComponentModel.DataAnnotations;

namespace AppJourneeMetier.Api.Models.Forms
{
    public class InscriptionForm
    {
        [Required]
        public int IdEvenement { get; set; }
        [Required]
        [StringLength(50)]
        public string? Nom { get; set; }
        [Required]
        [StringLength(50)]
        public string? Prenom { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
