using AppJourneeMetier.Api.Models;
using AppJourneeMetier.Api.Models.Forms;
using AppJourneeMetier.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppJourneeMetier.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InscriptionController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public InscriptionController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet("ByEvent/{idEvenement}")]
        public IActionResult GetByEvent(int idEvenement)
        {
            Command command = new Command("SELECT [IdInscription], [IdEvenement], [Nom], [Prenom], [Confirme], [Email] FROM [Inscription] WHERE [IdEvenement] = @IdEvenement;");
            command.AddParameter("IdEvenement", idEvenement);
            using (_dbConnection)
            {
                return Ok(_dbConnection.ExecuteReader(command, (dr) => new Inscription() { IdInscription = (int)dr["IdInscription"], IdEvenement = (int)dr["IdEvenement"], Nom = (string)dr["Nom"], Prenom = (string)dr["Prenom"], Confirme = (bool)dr["Confirme"], Email = (string)dr["Email"] }).ToList());
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] InscriptionForm formulaire)
        {
            Command command = new Command("INSERT INTO [Inscription] ([IdEvenement], [Nom], [Prenom], [Confirme], [Email]) OUTPUT INSERTED.[IdInscription] VALUES (@IdEvenement, @Nom, @Prenom, @Confirme, @Email);");
            command.AddParameter("IdEvenement", formulaire.IdEvenement);
            command.AddParameter("Nom", formulaire.Nom);
            command.AddParameter("Prenom", formulaire.Prenom);
            command.AddParameter("Confirme", false);
            command.AddParameter("Email", formulaire.Email);

            using (_dbConnection)
            {
                Inscription inscription = new Inscription() { IdEvenement = formulaire.IdEvenement, Nom = formulaire.Nom, Prenom = formulaire.Prenom, Email = formulaire.Email };
                object? result = _dbConnection.ExecuteScalar(command);
                if(result is null)
                {
                    return BadRequest();
                }

                inscription.IdInscription = (int)result;
                return Ok(inscription);
            }
        }

        [HttpPatch]
        public IActionResult Confirm([FromBody] ConfirmationForm formulaire)
        {
            Command command = new Command("UPDATE [Inscription] SET [Confirme] = @Confirme WHERE [IdInscription] = @IdInscription;");
            command.AddParameter("Confirme", formulaire.Confirme);
            command.AddParameter("IdInscription", formulaire.IdInscription);
            using (_dbConnection)
            {
                int rows = _dbConnection.ExecuteNonQuery(command);
                
                return rows == 1 ? NoContent() : BadRequest();
            }
        }
    }
}
