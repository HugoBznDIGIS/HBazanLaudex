using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        [Required(ErrorMessage = "Debe seleccionar un Estado")]
        public int IdEstado { get; set; }
        public string Estatus { get; set; }

        public static List<object> GetAll()
        {
            List<object> list = new List<object>();

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer("Server=.; Database= HBazanLaudex; TrustServerCertificate=True;Trusted_Connection=True;User ID=sa;Password=pass@word1;MultipleActiveResultSets=true");

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    // Aquí puedes realizar operaciones en tu contexto
                    var SP = context.Estados.FromSqlRaw("EstadoGetAll").ToList();

                    if (SP != null)
                    {
                        foreach (var itemEstado in SP)
                        {
                            Estado estado = new Estado();

                            estado.IdEstado = itemEstado.IdEstado;
                            estado.Estatus = itemEstado.Estatus;

                            list.Add(estado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }

            return list;
        }

    }
}
