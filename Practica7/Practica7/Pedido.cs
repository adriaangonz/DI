using System;
using System.Collections.Generic;
using System.Text;

namespace Practica7
{
    public class Pedido
    {
        public string Cliente { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string TipoRecogida { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }
}
