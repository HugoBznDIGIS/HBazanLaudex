﻿using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        [Required(ErrorMessage = "El Titulo es obligatorio")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "El nombre de usuario debe de ser menor a 50 caracteres y mayor a 1")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El descripcion es obligatorio")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "El nombre de usuario debe de ser menor a 50 caracteres y mayor a 1")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha debe es obligatoria")]
        public DateTime FechaVencimiento { get; set; }
        public bool Importante { get; set; }
        public Estado? Estado { get; set; }

        public static bool Add(Tarea tarea, int idUsuario, DataSourceProvider myConnection)
        {
            bool correct;

            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

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

        public static bool Delete(int idUsuario, int idTarea, int idUsuarioTarea, DataSourceProvider myConnection)
        {
            bool correct;

            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

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

        public static bool Update(BL.Tarea tarea, DataSourceProvider myConnection)
        {
            bool correct;

            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

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
