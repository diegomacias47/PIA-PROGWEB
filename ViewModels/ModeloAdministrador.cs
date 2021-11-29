using PROG.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG.ViewModels
{
    public class ModeloAdministrador
    {
       public List<Arma> lstA { get; set; }
       
        public List<Almacen> lstal { get; set; }
       public  ModeloUsuario usuario { get; set; }

        public List<ModeloArma> lstMA { get; set; }

    }
}
