using AppJourneeMetier.Api.Models;
using AppJourneeMetier.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppJourneeMetier.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EvenementController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public EvenementController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Command command = new Command("SELECT [IdEvenement], [DateDebut], [DateFin], [Titre], [Description], [IdCategorie], [Prix] FROM Evenement;");
            using(_dbConnection)
            {
                return Ok(_dbConnection.ExecuteReader(command, (dr) => new Evenement() { IdEvenement = (int)dr["IdEvenement"], DateDebut = (DateTime)dr["DateDebut"], DateFin = (DateTime)dr["DateFin"], Titre = (string)dr["Titre"], Description = (string)dr["Description"], IdCategorie = (int)dr["IdCategorie"], Prix = (decimal)dr["Prix"] }).ToList());
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Command command = new Command("SELECT [IdEvenement], [DateDebut], [DateFin], [Titre], [Description], [IdCategorie], [Prix] FROM Evenement WHERE IdEvenement = @IdEvenement;");
            command.AddParameter("IdEvenement", id);
            using (_dbConnection)
            {
                Evenement? evenement = _dbConnection.ExecuteReader(command, (dr) => new Evenement() { IdEvenement = (int)dr["IdEvenement"], DateDebut = (DateTime)dr["DateDebut"], DateFin = (DateTime)dr["DateFin"], Titre = (string)dr["Titre"], Description = (string)dr["Description"], IdCategorie = (int)dr["IdCategorie"], Prix = (decimal)dr["Prix"] }).SingleOrDefault();

                if (evenement is null)
                    return NotFound();

                return Ok(evenement);
            }
        }

        [HttpGet("ByCategory/{idCategorie}")]
        public IActionResult GetByCategory(int idCategorie)
        {
            throw new NotImplementedException();
        }
    }
}
