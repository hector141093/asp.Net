using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda1.Models;

namespace Tienda1.Repository
{
    class MemoryProductosRepository : IProductosRepository
    {
        private static List<Producto> productos;
        static MemoryProductosRepository()
        {
            productos= new List<Producto>();
        }        

        public void ActualizarProducto(Producto prod)
        {
            throw new NotImplementedException();
        }

        public void BorrarProducto(Producto prod)
        {
            throw new NotImplementedException();
        }

        public void CrearProducto(Producto nuevo)
        {
            if (productos.Count > 0)
            {
                nuevo.ID = productos.Max(p => p.ID);
            }else
            {
                nuevo.ID = 1;
            }
            productos.Add(nuevo);
        }

        public Producto LeerProductoPorID(int id)
        {
            return productos.FirstOrDefault(p => p.ID == id);
        }

        public List<Producto> LeerProductos()
        {
            return productos.ToList();
        }
    }
}
