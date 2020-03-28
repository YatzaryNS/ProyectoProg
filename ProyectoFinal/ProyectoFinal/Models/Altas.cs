using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Altas
    {
        [Key]
        public int IdAltas { set; get; }

        [Required]
        public int Ingreso_Id { set; get; }
        [ForeignKey("Ingreso_Id")]
        public Ingresos Ingresos { set; get; }

        [Required]
        public string Fecha_Salida { set; get; }

        public double Monto { set; get; }
    }
}