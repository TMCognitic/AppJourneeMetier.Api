using AppJourneeMetier.Api.Models;
using AppJourneeMetier.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppJourneeMetier.Api.Controllers
{
    [ApiController]
    [Route("api/Categorie")]
    public class CategorieController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public CategorieController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Command command = new Command("SELECT [IdCategorie], [Libelle] FROM [Categorie];");
            using (_dbConnection)
            {
                return Ok(_dbConnection.ExecuteReader(command, (dr) => new Categorie() { IdCategorie = (int)dr["IdCategorie"], Libelle = (string)dr["Libelle"] }).ToList());
            }
        }
    }
}
