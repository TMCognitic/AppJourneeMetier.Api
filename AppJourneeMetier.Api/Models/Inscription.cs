namespace AppJourneeMetier.Api.Models
{
    public class Inscription
    {
        //Database Columns : [IdInscription], [IdEvenement], [Nom], [Prenom], [Confirme], [Email]
        public int IdInscription { get; set; }
        public int IdEvenement { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public bool Confirme { get; set; }
        public string? Email { get; set; }
    }
}
