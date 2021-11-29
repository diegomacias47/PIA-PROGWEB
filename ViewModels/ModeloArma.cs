using Microsoft.AspNetCore.Http;
using PROG.Models.dbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROG.ViewModels
{
    public class ModeloArma
    {
        public int idArma { get; set; }

        [Required(ErrorMessage = "Se requiere de este valor")]
        [DisplayName("Nombre del arma:")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Se requiere de este valor")]
        [DisplayName("Tipo de arma:")]
        public int Tipo { get; set; }


        [Required(ErrorMessage = "Se requiere de este valor")]
        [DisplayName("Estado del arma:")]
        public int Estado { get; set; }


        [Required(ErrorMessage = "Se requiere de este valor")]
        [DisplayName("Precio del arma:")]
        public decimal Precio { get; set; }


        [Required(ErrorMessage = "Se requiere de este valor")]
        [DisplayName("Float del arma:")]
        public double? Float { get; set; }

        [DisplayName("Foto del arma:")]
        public IFormFile Foto { get; set; }

        public bool? Estatus { get; set; }


        public string dEstado { get; set; }

        public string dTipo { get; set; }

        public string dirFoto { get; set; }



        public List<Tipo> lsT { get; set; }
        public List<Estado> lstE { get; set; }
        public List<Arma> lstA { get; set; }


    }
}
