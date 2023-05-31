using ApiACME.Models.DataModels;
using ApiACME.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ApiACME.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidosServices _services;
        public PedidoController(PedidosServices services)
        {
            _services = services;
        }


        [HttpPost("JsonToXml")]
        public async Task<ActionResult<string>> JsonToXml(Pedido pedido)
        {
            var res = _services.JsonToXml(pedido);

            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

        // GET:
        [HttpGet]
        public async Task<string> GetStudents()
        {
            string url = "https://run.mocky.io/v3/19217075-6d4e-4818-98bc-416d1feb7b84";

            string response = await _services.GetInformationFromUrl(url);

            return response;
        }
    }
}
