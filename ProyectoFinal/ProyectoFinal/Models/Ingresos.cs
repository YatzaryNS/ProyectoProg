using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Ingresos
    {
        [Key]
        public int IdIngresos { set; get; }

        [Required]
        public int Paciente_Id { set; get; }
        [ForeignKey("Paciente_Id")]
        public Pacientes Pacientes { set; get; }

        [Required]
        public int Habitacion_Id { set; get; }
        [ForeignKey("Habitacion_Id")]
        public Habitaciones Habitaciones { set; get; }
        [Required]
        public string Fecha_Ingreso { set; get; }
    }
}