using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tienda1.Controllers;
using System.Web.Mvc;
using Tienda1.Models;
using System.Collections.Generic;
using Tienda1.Tests.Repositories;
using Tienda1.Repository;
using System.Linq;

namespace Tienda1.Tests.Controllers
{
    [TestClass]
    public class ProductosControllerTest
    {
        [TestMethod]
        public void Index_Retorna_Lista_de_Productos()
        {
            //Arrange
            var repo = PreparaRepositorioProductos();

            ProductosController controller = new ProductosController(repo);
            //Act
            var resultado = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(resultado, "Action Result no debe ser nulo");
            Assert.IsNotNull(resultado.Model, "Modelo no debe de ser nulo");
            //as es una manera más rapido, de castear un tipo
            //Siguiendo la regla de la herencia sino es devuelve null
            var model = resultado.Model as List<Producto>;
            Assert.IsNotNull(model, "Modelo no es Lista de producto");
            Assert.AreEqual(model.Count , 2, "Debe de contener todos los productos");

            
        }

        [TestMethod]
        public void Crear_GET_MuestraProductoVacio()
        {
            //Arrange
            var repo = PreparaRepositorioProductos();

            ProductosController controller = new ProductosController(repo);
            //Act
            var resultado = controller.Create() as ViewResult;
            //Assert 
            var model = resultado.Model as Producto;
            Assert.IsTrue(string.IsNullOrWhiteSpace(model.Descripcion), "Modelo tiene Descripción Vacía");
            //Hacer lo mismo con el resto de propiedades.
        }


        [TestMethod]
        public void Crear_POST_Guarda_ElProducto()
        {
            //Arrange
            var repo = PreparaRepositorioProductos();

            ProductosController controller = new ProductosController(repo);
            var productoNuevo = new Producto()
            {
                Precio = 100,
                Categoria = "Nuevo",
                Nombre = "Nuevo Nombre",
                Descripcion = "Nueva Descripcion",
                Disponibilidad = 100,
                Imagen = "Img.jpg"
            };

            var todosLosProds = repo.LeerProductos();
            //controller.ModelState.AddModelError("Descripcion", "Descripcion Vacia");
            //Act
            var resultado = controller.Create(productoNuevo,null) as RedirectToRouteResult;

            //Assert
            //TODO  este redirige a Index, tengo que verificar que se generó el nuevo producto
            Assert.IsNotNull(resultado, "Debe de redirigir a una Ruta");
            var accion = resultado.RouteValues["action"] ?? null;
            Assert.IsNotNull(accion,"Index", "Debe de redirigir a Index");
            var nuevosProds = repo.LeerProductos();
            Assert.AreNotEqual(todosLosProds.Count, nuevosProds.Count, "No debe de tener la misma cantidad de productos");
            Assert.AreEqual(todosLosProds.Count+1, nuevosProds.Count, "Debe de tener un producto Más");

            var diferentes = nuevosProds.Except(todosLosProds);
            Assert.IsNotNull(diferentes, "Deben de contener elementos diferentes");
            var nuevo = diferentes.FirstOrDefault();
            Assert.AreEqual(nuevo.Nombre, productoNuevo.Nombre, "Nombre de producto nuevo debe de coincidir");
        }
        [TestMethod]
        public void Crear_POST_NoPuedoCrearArticulos_SinDescripcion()
        {

            var repo = PreparaRepositorioProductos();
            ProductosController controller = new ProductosController(repo);
            var productoNuevo = new Producto()
            {
                Precio = 100,
                Categoria = "Nuevo",
                Nombre = "Nuevo Nombre",
                Descripcion = string.Empty,
                Disponibilidad = 100,
                Imagen = "Img.jpg"
            };

            var todosLosProds = repo.LeerProductos();
            controller.ModelState.AddModelError("Descripcion", "Descipcion Esta vacia");
           
            //Act
            var resultado = controller.Create(productoNuevo,null) as ViewResult;



            Assert.IsNotNull(resultado, "Action result (ViewResult) no debe ser nulo");
            Assert.IsNotNull(resultado.Model, "Modelo no debe de ser nulo");
            var model = resultado.Model as Producto;
            Assert.AreEqual(model.Categoria, productoNuevo.Categoria, "Debe de recordar los valores anteriores");
            Assert.IsFalse(controller.ModelState.IsValid,"ModelStates debe ser valido");
            //Assert.Inconclusive();//
            //Assert.Fail();
        }
        [TestMethod]
        public void Crear_POST_NoPuedoCrearSinImagen()
        {           
            var repo = PreparaRepositorioProductos();
            ProductosController controller = new ProductosController(repo);
            var productoNuevo = new Producto()
            {
                Precio = 100,
                Categoria = "Nuevo",
                Nombre = "Nuevo Nombre",
                Descripcion = string.Empty,
                Disponibilidad = 100,
                Imagen = string.Empty
            };

            var todosLosProds = repo.LeerProductos();
            controller.ModelState.AddModelError("Descripcion", "Descripcion Esta vacia");
            //Act
            var resultado = controller.Create(productoNuevo,null) as ViewResult;

            Assert.IsNotNull(resultado, "Action result (ViewResult) no debe ser nulo");
  //          Assert.Fail("No se pudo completar");
//            Assert.Inconclusive();
        }

        private IProductosRepository PreparaRepositorioProductos()
        {
            var repo = new MemoryProductosRepositoryTest();
            repo.CrearProducto(new Producto()
            {
                Descripcion = "A",
                Nombre = "A"
            });
            repo.CrearProducto(new Producto()
            {
                Descripcion = "B",
                Nombre = "B"
            });
            return repo;
        }   
    }
}