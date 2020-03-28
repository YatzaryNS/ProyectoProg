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
        public int IdHabitacion { set; get; }
        [Required]
        public int Numero { set; get; }
        [Required]
        public string Tipo { set; get; }
        [Required]
        public double Precio { set; get; }
    }
}