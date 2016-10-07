using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda1.Models;

namespace Tienda1.Repository
{
    public interface IProductosRepository
    {
        List<Producto> LeerProductos();
        Producto LeerProductoPorID(int id);
        void CrearProducto(Producto nuevo);
        void BorrarProducto(Producto prod);
        void ActualizarProducto(Producto prod);
    }
}
