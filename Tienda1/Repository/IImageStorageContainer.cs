using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda1.Repository
{
    interface IImageStorageContainer
    {
        string GuardarImagen(string contenedor, string nombre, Stream archivo);
        string LeerImagen(string contenedor, string nombre);
        
    }
}
