using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda1.Models;
using Tienda1.Repository;

namespace Tienda1.Controllers
{
    public class ProductosController : Controller
    {
        private IProductosRepository productos;
        public ProductosController(IProductosRepository prods)
        {
            productos = prods;
        }


        // GET: Productos
        public ActionResult Index()
        {
            var model = productos.LeerProductos();
            return View(model);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            var model = productos.LeerProductoPorID(id);
            return View(model);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            var model = new Producto(); 
            return View(model);
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Producto productoACrear, HttpPostedFileBase imagen)
        {
            var Url = string.Empty;
            if(imagen==null || string.IsNullOrWhiteSpace(imagen.FileName) )
            {
                ModelState.AddModelError("Imagen", "Debe de subir un archivo");
            }
            else
            {
                Url = "~\\imagenesproductos\\" + imagen.FileName;
                var destino = this.HttpContext.Server.MapPath(Url);
                try
                {
                   imagen.SaveAs(destino);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Imagen", "Imagen no se guardo" + ex.Message);
                }
            }
            //El modelBinder es el que hace esta magia
            //El modelState se encuentra en el controlador 
            //Propiedad por la cual se comunica modelBinder
            if (!ModelState.IsValid)
            {
               return View(productoACrear);
            }
            try
            {
                // TODO: Add insert logic here
                productoACrear.Imagen = Url;
                productos.CrearProducto(productoACrear);
                //Guardar imagen en algun lugar......
                //return View(productoACrear);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            var model = productos.LeerProductoPorID(id);
            return View(model);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
