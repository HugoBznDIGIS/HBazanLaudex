using DL;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioTareaController : Controller
    {
        private readonly IConfiguration _configuration;
        private static DataSourceProvider instancia;
        public UsuarioTareaController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataSourceProvider ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new DataSourceProvider(_configuration);
            }
            return instancia;
        }
        public IActionResult GetAll()
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            List<object> list = BL.UsuarioTarea.GetAll((int)HttpContext.Session.GetInt32("IdUsuario"), myConnection);
            BL.UsuarioTarea usuarioTarea = new BL.UsuarioTarea();
            usuarioTarea.UsuariosTareas = list;

            if (usuarioTarea.UsuariosTareas != null)
            {
                return View(usuarioTarea);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Importante()
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            List<object> list = BL.UsuarioTarea.GetImportante((int)HttpContext.Session.GetInt32("IdUsuario"), myConnection);
            BL.UsuarioTarea usuarioTarea = new BL.UsuarioTarea();
            usuarioTarea.UsuariosTareas = list;

            if (usuarioTarea.UsuariosTareas != null)
            {
                return View(usuarioTarea);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Completado()
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            List<object> list = BL.UsuarioTarea.GetCompletado((int)HttpContext.Session.GetInt32("IdUsuario"), myConnection);
            BL.UsuarioTarea usuarioTarea = new BL.UsuarioTarea();
            usuarioTarea.UsuariosTareas = list;

            if (usuarioTarea.UsuariosTareas != null)
            {
                return View(usuarioTarea);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Pendiente()
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            List<object> list = BL.UsuarioTarea.GetPendiente((int)HttpContext.Session.GetInt32("IdUsuario"), myConnection);
            BL.UsuarioTarea usuarioTarea = new BL.UsuarioTarea();
            usuarioTarea.UsuariosTareas = list;

            if (usuarioTarea.UsuariosTareas != null)
            {
                return View(usuarioTarea);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Form(BL.Tarea tarea, int estado)
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            bool correct;
            tarea.Estado = new BL.Estado();
            tarea.Estado.IdEstado = estado;

            if (tarea.IdTarea == 0)
            {
                correct = BL.Tarea.Add(tarea, (int)HttpContext.Session.GetInt32("IdUsuario"), myConnection);

                if (correct)
                {
                    ViewBag.Mensaje = "Se ha agregado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error";
                }
            }
            else
            {
                // UPDATE
                correct = BL.Tarea.Update(tarea, myConnection);

                if (correct)
                {
                    ViewBag.Mensaje = "Se ha actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error";
                }
            }

            return PartialView("Modal");
        }

        public IActionResult Delete(int idTarea, int idUsuarioTarea)
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            bool correct = BL.Tarea.Delete((int)HttpContext.Session.GetInt32("IdUsuario"), idTarea, idUsuarioTarea, myConnection);

            if (correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error";
            }

            return PartialView("Modal");
        }

        public IActionResult GetById(int idTarea)
        {
            DataSourceProvider myConnection = ObtenerInstancia();

            BL.UsuarioTarea usuarioTarea = BL.UsuarioTarea.GetById((int)HttpContext.Session.GetInt32("IdUsuario"), idTarea, myConnection);

            if (usuarioTarea == null || usuarioTarea.Tarea == null)
            {
                return NotFound();
            }

            return Json(new
            {
                IdTarea = usuarioTarea.Tarea.IdTarea,
                Titulo = usuarioTarea.Tarea.Titulo,
                Descripcion = usuarioTarea.Tarea.Descripcion,
                FechaVencimiento = usuarioTarea.Tarea.FechaVencimiento,
                Estado = usuarioTarea.Tarea.Estado,
                Importante = usuarioTarea.Tarea.Importante
            });
        }
    }
}
