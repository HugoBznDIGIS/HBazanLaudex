using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioTareaController : Controller
    {
        public IActionResult GetAll()
        {
            List<object> list = BL.UsuarioTarea.GetAll((int)HttpContext.Session.GetInt32("IdUsuario"));
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
            List<object> list = BL.UsuarioTarea.GetImportante((int)HttpContext.Session.GetInt32("IdUsuario"));
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
            List<object> list = BL.UsuarioTarea.GetCompletado((int)HttpContext.Session.GetInt32("IdUsuario"));
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
            List<object> list = BL.UsuarioTarea.GetPendiente((int)HttpContext.Session.GetInt32("IdUsuario"));
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
            bool correct;
            tarea.Estado = new BL.Estado();
            tarea.Estado.IdEstado = estado;

            if (tarea.IdTarea == 0)
            {
                correct = BL.Tarea.Add(tarea, (int)HttpContext.Session.GetInt32("IdUsuario"));

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
                correct = BL.Tarea.Update(tarea);

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
            bool correct = BL.Tarea.Delete((int)HttpContext.Session.GetInt32("IdUsuario"), idTarea, idUsuarioTarea);

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
            BL.UsuarioTarea usuarioTarea = BL.UsuarioTarea.GetById((int)HttpContext.Session.GetInt32("IdUsuario"), idTarea);

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
