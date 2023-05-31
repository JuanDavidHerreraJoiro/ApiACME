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
