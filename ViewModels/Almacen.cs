using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG.ViewModels
{
    public class Almacen
    {
        public string descripcion;
        public int tamanio;

        public string des { get; set; }
        public int tam { get; set; }

        public Almacen()
        {

        }
        public Almacen(string descripcion, int tamanio)
        {
            this.descripcion = descripcion;
            this.tamanio = tamanio;

        }

        public string getDescripcion()
        {
            return descripcion ;
        }

        public int getTamanio()
        {
            return tamanio;
        }
            

    }
}
