using ApiACME.Models.DataModels;
using ApiACME.Services;
using Microsoft.AspNetCore.Mvc;

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


        /// <summary>
        /// This is a C# function that converts a JSON object to an XML string and returns it as an
        /// ActionResult.
        /// </summary>
        /// <param name="Pedido">Pedido is a class or a model that represents an order or a request. It
        /// contains properties and methods that define the attributes and behavior of the order. In
        /// this case, it is used as a parameter for the JsonToXml method, which converts a JSON object
        /// to an XML string. The properties of</param>
        /// <returns>
        /// A `Task` that will eventually return an `ActionResult<string>` containing the result of
        /// converting a `Pedido` object from JSON to XML format. If the conversion is successful, the
        /// `ActionResult<string>` will contain the XML string. If the conversion fails, the
        /// `ActionResult<string>` will be a `NotFound` response.
        /// </returns>
        [HttpPost("JsonToXml")]
        public Task<ActionResult<string>> JsonToXml(Pedido pedido)
        {
            var res = _services.JsonToXml(pedido);

            if (res == null)
            {
                return Task.FromResult<ActionResult<string>>(NotFound());
            }

            return Task.FromResult<ActionResult<string>>(res);
        }

        // GET:
        /// <summary>
        /// This C# function retrieves student information from a specified URL using an HTTP GET
        /// request.
        /// </summary>
        /// <returns>
        /// The method `GetStudents()` returns a `Task<string>` object, which represents an asynchronous
        /// operation that returns a string. The string returned is the response obtained from the URL
        /// specified in the `url` variable.
        /// </returns>
        [HttpGet]
        public async Task<string> GetStudents()
        {
            string url = "https://run.mocky.io/v3/19217075-6d4e-4818-98bc-416d1feb7b84";

            string response = await _services.GetInformationFromUrl(url);

            return response;
        }
    }
}
