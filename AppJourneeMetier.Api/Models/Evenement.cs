namespace AppJourneeMetier.Api.Models
{
    public class Evenement
    {
        //Database Columns [IdEvenement], [DateDebut], [DateFin], [Titre], [Description], [IdCategorie], [Prix]
        public int IdEvenement { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string? Titre { get; set; }
        public string? Description { get; set; }
        public int IdCategorie { get; set; }
        public decimal Prix { get; set; }
    }
}
