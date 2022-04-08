using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private IAmigoAlmacen amigoAlmacen;
        private IHostingEnvironment hosting;

        // CONSTRUCTOR
        public HomeController(IAmigoAlmacen AmigoAlmacen, IHostingEnvironment hostingEnvironment)
        {
            amigoAlmacen = AmigoAlmacen;
            hosting = hostingEnvironment;
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ViewResult Index()
        {
            var modelo = amigoAlmacen.getAllFriends();
            return View(modelo);
        }

        [Route("Home/Details/{id?}")]
        public ViewResult Details(int id)
        {
            DetallesView detalles = new DetallesView();
            detalles.amigo = amigoAlmacen.getDataFriend(id);
            detalles.Titulo = "ESTE ES EL TITULO";
            detalles.Subtitulo = "Este es el subtitulo";

          /*  if (detalles.amigo == null) {

                Response.StatusCode = 404;
                return View("AmigoNoEncontrado", id);

            }*/
            return View(detalles);
        }

        //Metodo que se llama al abrir la vista Crear Usuarios
        [HttpGet]
        [Route("Home/Create")]
        public ViewResult Create()
        {
            return View();
        }

        // METODO DE CREACION
        [HttpPost]
        public IActionResult Create(CrearAmigoModelo a)
        {
            if (ModelState.IsValid)
            {
                string guidImage = null;

                if (a.Foto != null)
                {
                    string ficherosImagenes = Path.Combine(hosting.WebRootPath, "images");
                    guidImage = Guid.NewGuid().ToString() + a.Foto.FileName;

                    string rutaDefinitiva = Path.Combine(ficherosImagenes, guidImage);

                    var fileStream = new FileStream(rutaDefinitiva, FileMode.Create);

                    a.Foto.CopyTo(fileStream);
                    
                    fileStream.Close();
                }

                // Guardamos en base de datos
                Amigo nuevoAmigo = new Amigo();
                nuevoAmigo.Nombre = a.Nombre;
                nuevoAmigo.Email = a.Email;
                nuevoAmigo.Ciudad = a.Ciudad;
                nuevoAmigo.rutaFoto = guidImage;

                amigoAlmacen.nuevo(nuevoAmigo);

                return RedirectToAction("details", new { id = nuevoAmigo.Id });
            }
            return View();
        }

        // METODO DE MODIFICACION
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Amigo amigo = amigoAlmacen.getDataFriend(id);
            EditarAmigoModelo amigoEditar = new EditarAmigoModelo
            {
                Id = amigo.Id,
                Nombre = amigo.Nombre,
                Email = amigo.Email,
                Ciudad = amigo.Ciudad,
                rutaFotoExistente = amigo.rutaFoto
        };
        return View(amigoEditar);
        }

        [HttpPost]
        public IActionResult Edit(EditarAmigoModelo model)
        {
            if (ModelState.IsValid) {

                // Obtenemos los datos de nuestro amigo de la base de datos
                Amigo amigo = amigoAlmacen.getDataFriend(model.Id);

                // Actualizamos los datos del objeto
                amigo.Nombre = model.Nombre;
                amigo.Email = model.Email;
                amigo.Ciudad = model.Ciudad;

                if(model.Foto != null){ 

                    if (model.rutaFotoExistente != null) {

                        string ruta = Path.Combine(hosting.WebRootPath, "images", model.rutaFotoExistente);
                        System.IO.File.Delete(ruta);
                    }

                amigo.rutaFoto = SubirImagen(model);
                } 

                Amigo amigoModificado = amigoAlmacen.modificar(amigo);
                return RedirectToAction("index");
            
            }
            return View(model);
        }

        private string SubirImagen(EditarAmigoModelo model)
        {

            string nombreFichero = null;

            if (model.Foto != null) {

                string carpetaSubida = Path.Combine(hosting.WebRootPath, "images");
                nombreFichero = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                string ruta = Path.Combine(carpetaSubida, nombreFichero);

                using (var fileStream = new FileStream(ruta, FileMode.Create))
                {
                    model.Foto.CopyTo(fileStream);
                    fileStream.Close();

                }
            }
        return nombreFichero;
        }

        // METODO DE BORRADO
        public IActionResult Delete(Amigo a)
        {
            Amigo amigo = amigoAlmacen.borrar(a.Id);
             return RedirectToAction("index");
            
        }

    }
}
   

