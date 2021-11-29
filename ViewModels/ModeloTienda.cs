using PROG.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG.ViewModels
{
    public class ModeloTienda
    {
       public ModeloUsuario usuario { get; set; }
       public ModeloArma arma { get; set; }

       public List<Arma> lstA { get; set; }



    }
}
