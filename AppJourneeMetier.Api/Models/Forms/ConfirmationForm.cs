using System.ComponentModel.DataAnnotations;

namespace AppJourneeMetier.Api.Models.Forms
{
    public class ConfirmationForm
    {
        [Required]
        public int IdInscription { get; set; }
        [Required]
        public bool Confirme { get; set; }
    }
}
