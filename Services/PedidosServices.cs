using ApiACME.Models.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ApiACME.Services
{
    public class PedidosServices
    {
        public async Task<string> GetInformationFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    string json = XmlToJson(responseBody);
                    return json;
                }
                else
                {
                    throw new Exception("Error al obtener la respuesta: " + response.StatusCode);
                }
            }
        }

        /// <summary>
        /// The function converts an XML string to a JSON string with specific key-value pairs.
        /// </summary>
        /// <param name="xml">a string containing an XML document.</param>
        /// <returns>
        /// The method returns a JSON string that contains the values of the "Codigo" and "Mensaje"
        /// nodes from the input XML string. The JSON string has the following format:
        /// </returns>
        private string XmlToJson(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNode codigoNode = xmlDoc.SelectSingleNode("//Codigo");
            XmlNode mensajeNode = xmlDoc.SelectSingleNode("//Mensaje");

            string codigo = codigoNode?.InnerText ?? string.Empty;
            string mensaje = mensajeNode?.InnerText ?? string.Empty;

            string json = $"{{ \"enviarPedidoRespuesta\": {{ \"codigoEnvio\": \"{codigo}\", \"estado\": \"{mensaje}\" }} }}";

            return json;
        }

        /// <summary>
        /// The function converts a given Pedido object into an XML string with specific formatting.
        /// </summary>
        /// <param name="Pedido">The "Pedido" parameter is an object of a class named "Pedido" which
        /// contains properties such as "numPedido", "cantidadPedido", "codigoEAN", "nombreProducto",
        /// "numDocumento", and "direccion". These properties hold the values of a specific order that
        /// needs to be converted from</param>
        /// <returns>
        /// The method is returning a string representation of an XML document that contains the
        /// information of a Pedido object in a specific format.
        /// </returns>
        public string JsonToXml(Pedido pedido)
        {
            var pedidoXml = new XElement("soapenv.Envelope",
                new XAttribute("xmlns.soapenv", "http://schemas.xmlsoap.org/soap/envelope/"),
                new XAttribute("xmlns.env", "http://WSDLs/EnvioPedidos/EnvioPedidosAcme"),
                new XElement("soapenv.Header"),
                new XElement("soapenv.Body",
                    new XElement("env.nvioPedidoAcme",
                        new XElement("EnvioPedidoRequest",
                            new XElement("pedido", pedido.numPedido),
                            new XElement("Cantidad", pedido.cantidadPedido),
                            new XElement("EAN", pedido.codigoEAN),
                            new XElement("Producto", pedido.nombreProducto),
                            new XElement("Cedula", pedido.numDocumento),
                            new XElement("Direccion", pedido.direccion)
                        )
                    )
                )
            );

            // Convertir XML a string
            string xmlString = pedidoXml.ToString();

            Console.WriteLine(xmlString);

            return xmlString;
        }
    }
}
