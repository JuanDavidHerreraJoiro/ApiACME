namespace ApiACME.Models.DataModels
{
    /* The class "Pedido" contains properties for a product order, including the order number,
    quantity, product code, name, customer document number, and delivery address. */
    public class Pedido
    {
        public string numPedido { get; set; }
        public string cantidadPedido { get; set; }
        public string codigoEAN { get; set; }
        public string nombreProducto { get; set; }
        public string numDocumento { get; set; }
        public string direccion { get; set; }
    }
}
