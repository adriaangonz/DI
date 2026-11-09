using System;
using System.Collections.Generic;
using System.Text;

namespace Practica6
{
    public class Reserva
    {
        public string Nombre { get; set; }
        public string Actividad { get; set; }
        public DateTime Fecha { get; set; }
        public string Turno { get; set; }
        public int Plazas { get; set; }
        //tener fecha formateadsa
        public string FechaFormateada => Fecha.ToString("dd/MM/yyyy");
    }
}
