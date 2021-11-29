using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROG.ViewModels
{
    public class ModeloUsuario
    {
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Se requiere de este valor")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Se requiere de este valor")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Se requiere de este valor")]
        public string Contrasena { get; set; }

        public decimal? Fondos { get; set; }

        public int? Rol { get; set; }




    }
}
