using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        // PROPS
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public List<object>? Usuarios { get; set; }
        public bool Correct { get; set; }

        // METHODS
        public static bool Add(Usuario usuario, DataSourceProvider myConnection)
        {
            bool correct;

            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    int affectedRow = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Username}', @Password", new SqlParameter("@Password", usuario.Password));

                    if (affectedRow > 0)
                    {
                        correct = true;
                    }
                    else
                    {
                        correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }

            return correct;
        }

        public static Usuario GetByUsername(string username, DataSourceProvider myConnection)
        {
            Usuario usuario = new Usuario();

            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    var SP = context.Usuarios.FromSqlRaw($"UsuarioGetByUsername '{username}'").AsEnumerable().SingleOrDefault();

                    if (SP != null)
                    {
                        usuario.IdUsuario = SP.IdUsuario;
                        usuario.Username = SP.Username;
                        usuario.Password = SP.Password;

                        usuario.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }

            return usuario;
        }
    }
}