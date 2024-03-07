using Microsoft.AspNetCore.Mvc;
using db_Api.Models.Generated;
using db_Api.Models.Generated.Reunion;
//using db_Api.Repository.OutilsRepository;
using db_Api.Repository.ReunionsRepository;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReunionController : ControllerBase
    {
        private readonly IReunionRepository _reunionRepository;
        //private readonly IOutilRepository _outilRepository;

        public ReunionController(IReunionRepository reunionRepository)
        //IOutilRepository outilRepository)
        {
            _reunionRepository = reunionRepository;
        //    _outilRepository = outilRepository;
        }


               [HttpGet]
        [Route("getListeReunion11")]
        public ActionResult<IEnumerable<ListeReunion11>> GetListeReunion11(string? numReunion, string? type, string? lieu, string? site)
        {

            var items = _reunionRepository.liste_reunion11( numReunion, type, lieu, site);
            items.Reverse();
            return Ok(items);

        }

        [HttpPost]
        [Route("add_reunion11")]
        public async Task<ActionResult<NumeroReunion>> AddReunion11(string? typeR, DateTime? date_prev, DateTime? date_Real,string? lieu, string? site)
        {
            try
            {
            var items = await _reunionRepository.add_reunion11(typeR, date_prev, date_Real, lieu, site);
            NumeroReunion numeroReunion = new NumeroReunion();
            numeroReunion.nReunion = Int32.Parse(items[0].ok);
            return numeroReunion;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteReunion")]
        public ActionResult<Delete> DeleteReunion(int nReunion)
        {
            try
            {
                var response = _reunionRepository.DeleteReunion(nReunion);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("updateReunion")]
        public async Task<ActionResult> updateReunion(updatereunion Am)
        {
            try
            {
                var items = await _reunionRepository.update_reunion(Am);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}