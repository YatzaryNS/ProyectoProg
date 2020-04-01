using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Habitaciones
    {
        [Key]
        public int IdHabitacion { get; set; } 
        [Required]
        public int Numero { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public TipoHab Tipo { get; set; }

        public enum TipoHab
        {
            Doble,Suite,Privada
        }
    }
} 