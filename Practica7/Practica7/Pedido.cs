using System;
using System.Collections.Generic;
using System.Text;

namespace Practica7
{
    internal class Pedido
    {
        public string nombre { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }
        public string tipoRecogida { get; set; }
        public string observaciones { get; set; }
    }
}
