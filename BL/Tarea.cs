using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Importante { get; set; }
        public Estado? Estado { get; set; }

        public static bool Add(Tarea tarea, int idUsuario)
        {
            bool correct;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer("Server=.; Database= HBazanLaudex; TrustServerCertificate=True;Trusted_Connection=True;User ID=sa;Password=pass@word1;MultipleActiveResultSets=true");

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    int affectedRow = context.Database.ExecuteSqlRaw($"TareaAdd '{tarea.Titulo}', '{tarea.Descripcion}', '{tarea.FechaVencimiento}', {tarea.Importante}, {tarea.Estado.IdEstado}, {idUsuario}");

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

        public static bool Delete(int idUsuario, int idTarea, int idUsuarioTarea)
        {
            bool correct;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer("Server=.; Database= HBazanLaudex; TrustServerCertificate=True;Trusted_Connection=True;User ID=sa;Password=pass@word1;MultipleActiveResultSets=true");

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    int affectedRow = context.Database.ExecuteSqlRaw($"UsuarioTareaDelete {idUsuario}, {idTarea}, {idUsuarioTarea}");

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

        public static bool Update(BL.Tarea tarea)
        {
            bool correct;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer("Server=.; Database= HBazanLaudex; TrustServerCertificate=True;Trusted_Connection=True;User ID=sa;Password=pass@word1;MultipleActiveResultSets=true");

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    int affectedRow = context.Database.ExecuteSqlRaw($"TareaUpdate '{tarea.Titulo}', '{tarea.Descripcion}', '{tarea.FechaVencimiento}', {tarea.Importante}, {tarea.Estado.IdEstado}, {tarea.IdTarea}");

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
    }
}
