using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Citas
    {
        [Key]
        public int IdCitas { set; get; }

        [Required]
        public int Paciente_Id { set; get; }
        [ForeignKey("Paciente_Id")]
        public Pacientes Pacientes { set; get; }

        [Required]
        public int Medico_Id { set; get; }
        [ForeignKey("Medico_Id")]
        public Medicos Medicos { set; get; }
        [Required]
        public string Fecha { set; get; }

        [Required]
        public string Hora { set; get; }
    }
}