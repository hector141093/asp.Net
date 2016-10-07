using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tienda1.Models
{
    public class Producto
    {
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public decimal Disponibilidad { get; set; }
        public int ID { get; set; }
        public string Categoria { get; set; }
        public int Precio { get; set; }
        public string Imagen { get; set; }
    }
}