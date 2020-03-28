using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Pacientes
    {
        [Key]
        public int IdPacientes { set; get; }
        [Required]
        public string Cedula { set; get; }
        [Required]
        public string Nombre { set; get; }
        [Required]
        public bool Asegurado { set; get; }
    }
}