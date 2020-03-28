using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Medicos
    {
        [Key]
        public int IdMedicos { set; get; }
        [Required]
        public string Nombre { set; get; }
        [Required]
        public int Exequatur { set; get; }
        [Required]
        public string Especialidad { set; get; }
    }
}