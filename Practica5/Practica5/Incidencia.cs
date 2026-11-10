using System;
using System.Collections.Generic;
using System.Text;

namespace Practica5
{
    public class Incidencia
    {
        public string Titulo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public int Tiempo { get; set; }
        // El estado inicial de toda nueva incidencia será Pendiente
        public string Estado { get; set; } = "Pendiente";
    }
}
