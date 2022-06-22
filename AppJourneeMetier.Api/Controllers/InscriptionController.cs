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
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult Confirm()
        {
            throw new NotImplementedException();
        }
    }
}
