using DL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private static DataSourceProvider instancia;
        public LoginController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataSourceProvider ObtenerCadena()
        {
            if (instancia == null)
            {
                instancia = new DataSourceProvider(_configuration);
            }
            return instancia;
        }

        public IActionResult LogIn()
        {
            HttpContext.Session.Remove("IdUsuario");
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string username, string psswd)
        {
            DataSourceProvider myConnection = ObtenerCadena();

            var bcrypt = new Rfc2898DeriveBytes(psswd, new byte[0], 10000, HashAlgorithmName.SHA256);
            var passwordHash = bcrypt.GetBytes(20);

            BL.Usuario usuarioGetByEmail = BL.Usuario.GetByUsername(username, myConnection);

            if (usuarioGetByEmail.Correct)
            {
                if (usuarioGetByEmail.Password.SequenceEqual(passwordHash))
                {
                    HttpContext.Session.SetInt32("IdUsuario", usuarioGetByEmail.IdUsuario);
                    return RedirectToAction("GetAll", "UsuarioTarea");
                }
                else
                {
                    ViewBag.Mensaje = "Ingrese una contraseña valida";
                }
            }
            else
            {
                ViewBag.Mensaje = "Ingrese un correo valido";
            }

            return PartialView("Modal");
        }

        [HttpPost]
        public IActionResult Register(BL.Usuario usuario, string psswd) 
        {
            DataSourceProvider myConnection = ObtenerCadena();

            var bcrypt = new Rfc2898DeriveBytes(psswd, new byte[0], 10000, HashAlgorithmName.SHA256);
            var passwordHash = bcrypt.GetBytes(20);
            usuario.Password = passwordHash;

            bool correct = BL.Usuario.Add(usuario, myConnection);

            if (correct)
            {
                ViewBag.Mensaje = "Se ha agregado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ha ocurrido un error";
            }

            return PartialView("Modal");
        }
    }
}
