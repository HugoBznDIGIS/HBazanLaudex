using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioTarea
    {
        public int IdUsuarioTarea { get; set; }
        public Usuario Usuario { get; set; }
        public Tarea Tarea { get; set; }
        public List<object> UsuariosTareas { get; set; }

        public static List<object> GetAll(int idUsuario, DataSourceProvider myConnection)
        {
            List<object> list = new List<object>();
            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    var SP = (from objUsuarioTareas in context.UsuarioTareas
                              join objUsuario in context.Usuarios on objUsuarioTareas.IdUsuario equals objUsuario.IdUsuario
                              join objTarea in context.Tareas on objUsuarioTareas.IdTarea equals objTarea.IdTarea
                              where objUsuarioTareas.IdUsuario == idUsuario
                              select new
                              {
                                  IdUsuarioTareas = objUsuarioTareas.IdUsuarioTarea,
                                  IdUsuario = objUsuario.IdUsuario,
                                  Username = objUsuario.Username,
                                  IdTarea = objTarea.IdTarea,
                                  Titulo = objTarea.Titulo,
                                  Descripcion = objTarea.Descripcion,
                                  FechaVencimiento = objTarea.FechaVencimiento,
                                  Importante = objTarea.Importante,
                                  IdEstado = objTarea.IdEstado
                              }).ToList();


                    if (SP != null)
                    {
                        foreach (var itemUsuarioTarea in SP)
                        {
                            UsuarioTarea usuarioTarea = new UsuarioTarea();

                            usuarioTarea.IdUsuarioTarea = itemUsuarioTarea.IdUsuarioTareas;
                            usuarioTarea.Usuario = new Usuario();
                            usuarioTarea.Usuario.IdUsuario = (int)itemUsuarioTarea.IdUsuario;
                            usuarioTarea.Usuario.Username = itemUsuarioTarea.Username;
                            usuarioTarea.Tarea = new Tarea();
                            usuarioTarea.Tarea.IdTarea = itemUsuarioTarea.IdTarea;
                            usuarioTarea.Tarea.Titulo = itemUsuarioTarea.Titulo;
                            usuarioTarea.Tarea.Descripcion = itemUsuarioTarea.Descripcion;
                            usuarioTarea.Tarea.FechaVencimiento = itemUsuarioTarea.FechaVencimiento;
                            usuarioTarea.Tarea.Importante = itemUsuarioTarea.Importante;
                            usuarioTarea.Tarea.Estado = new Estado();
                            usuarioTarea.Tarea.Estado.IdEstado = itemUsuarioTarea.IdEstado;

                            list.Add(usuarioTarea);
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

        public static List<object> GetImportante(int idUsuario, DataSourceProvider myConnection)
        {
            List<object> list = new List<object>();
            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    var SP = (from objUsuarioTareas in context.UsuarioTareas
                              join objUsuario in context.Usuarios on objUsuarioTareas.IdUsuario equals objUsuario.IdUsuario
                              join objTarea in context.Tareas on objUsuarioTareas.IdTarea equals objTarea.IdTarea
                              where objTarea.Importante == true && objUsuario.IdUsuario == idUsuario
                              select new
                              {
                                  IdUsuarioTareas = objUsuarioTareas.IdUsuarioTarea,
                                  IdUsuario = objUsuario.IdUsuario,
                                  Username = objUsuario.Username,
                                  IdTarea = objTarea.IdTarea,
                                  Titulo = objTarea.Titulo,
                                  Descripcion = objTarea.Descripcion,
                                  FechaVencimiento = objTarea.FechaVencimiento,
                                  Importante = objTarea.Importante,
                                  IdEstado = objTarea.IdEstado
                              }).ToList();


                    if (SP != null)
                    {
                        foreach (var itemUsuarioTarea in SP)
                        {
                            UsuarioTarea usuarioTarea = new UsuarioTarea();

                            usuarioTarea.IdUsuarioTarea = itemUsuarioTarea.IdUsuarioTareas;
                            usuarioTarea.Usuario = new Usuario();
                            usuarioTarea.Usuario.IdUsuario = (int)itemUsuarioTarea.IdUsuario;
                            usuarioTarea.Usuario.Username = itemUsuarioTarea.Username;
                            usuarioTarea.Tarea = new Tarea();
                            usuarioTarea.Tarea.IdTarea = itemUsuarioTarea.IdTarea;
                            usuarioTarea.Tarea.Titulo = itemUsuarioTarea.Titulo;
                            usuarioTarea.Tarea.Descripcion = itemUsuarioTarea.Descripcion;
                            usuarioTarea.Tarea.FechaVencimiento = itemUsuarioTarea.FechaVencimiento;
                            usuarioTarea.Tarea.Importante = itemUsuarioTarea.Importante;
                            usuarioTarea.Tarea.Estado = new Estado();
                            usuarioTarea.Tarea.Estado.IdEstado = itemUsuarioTarea.IdEstado;

                            list.Add(usuarioTarea);
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

        public static List<object> GetCompletado(int idUsuario, DataSourceProvider myConnection)
        {
            List<object> list = new List<object>();
            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    var SP = (from objUsuarioTareas in context.UsuarioTareas
                              join objUsuario in context.Usuarios on objUsuarioTareas.IdUsuario equals objUsuario.IdUsuario
                              join objTarea in context.Tareas on objUsuarioTareas.IdTarea equals objTarea.IdTarea
                              where objUsuario.IdUsuario == idUsuario && objTarea.Estado.IdEstado == 1
                              select new
                              {
                                  IdUsuarioTareas = objUsuarioTareas.IdUsuarioTarea,
                                  IdUsuario = objUsuario.IdUsuario,
                                  Username = objUsuario.Username,
                                  IdTarea = objTarea.IdTarea,
                                  Titulo = objTarea.Titulo,
                                  Descripcion = objTarea.Descripcion,
                                  FechaVencimiento = objTarea.FechaVencimiento,
                                  Importante = objTarea.Importante,
                                  IdEstado = objTarea.IdEstado
                              }).ToList();


                    if (SP != null)
                    {
                        foreach (var itemUsuarioTarea in SP)
                        {
                            UsuarioTarea usuarioTarea = new UsuarioTarea();

                            usuarioTarea.IdUsuarioTarea = itemUsuarioTarea.IdUsuarioTareas;
                            usuarioTarea.Usuario = new Usuario();
                            usuarioTarea.Usuario.IdUsuario = (int)itemUsuarioTarea.IdUsuario;
                            usuarioTarea.Usuario.Username = itemUsuarioTarea.Username;
                            usuarioTarea.Tarea = new Tarea();
                            usuarioTarea.Tarea.IdTarea = itemUsuarioTarea.IdTarea;
                            usuarioTarea.Tarea.Titulo = itemUsuarioTarea.Titulo;
                            usuarioTarea.Tarea.Descripcion = itemUsuarioTarea.Descripcion;
                            usuarioTarea.Tarea.FechaVencimiento = itemUsuarioTarea.FechaVencimiento;
                            usuarioTarea.Tarea.Importante = itemUsuarioTarea.Importante;
                            usuarioTarea.Tarea.Estado = new Estado();
                            usuarioTarea.Tarea.Estado.IdEstado = itemUsuarioTarea.IdEstado;

                            list.Add(usuarioTarea);
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

        public static List<object> GetPendiente(int idUsuario, DataSourceProvider myConnection)
        {
            List<object> list = new List<object>();
            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    var SP = (from objUsuarioTareas in context.UsuarioTareas
                              join objUsuario in context.Usuarios on objUsuarioTareas.IdUsuario equals objUsuario.IdUsuario
                              join objTarea in context.Tareas on objUsuarioTareas.IdTarea equals objTarea.IdTarea
                              where objUsuario.IdUsuario == idUsuario && objTarea.Estado.IdEstado == 2
                              select new
                              {
                                  IdUsuarioTareas = objUsuarioTareas.IdUsuarioTarea,
                                  IdUsuario = objUsuario.IdUsuario,
                                  Username = objUsuario.Username,
                                  IdTarea = objTarea.IdTarea,
                                  Titulo = objTarea.Titulo,
                                  Descripcion = objTarea.Descripcion,
                                  FechaVencimiento = objTarea.FechaVencimiento,
                                  Importante = objTarea.Importante,
                                  IdEstado = objTarea.IdEstado
                              }).ToList();


                    if (SP != null)
                    {
                        foreach (var itemUsuarioTarea in SP)
                        {
                            UsuarioTarea usuarioTarea = new UsuarioTarea();

                            usuarioTarea.IdUsuarioTarea = itemUsuarioTarea.IdUsuarioTareas;
                            usuarioTarea.Usuario = new Usuario();
                            usuarioTarea.Usuario.IdUsuario = (int)itemUsuarioTarea.IdUsuario;
                            usuarioTarea.Usuario.Username = itemUsuarioTarea.Username;
                            usuarioTarea.Tarea = new Tarea();
                            usuarioTarea.Tarea.IdTarea = itemUsuarioTarea.IdTarea;
                            usuarioTarea.Tarea.Titulo = itemUsuarioTarea.Titulo;
                            usuarioTarea.Tarea.Descripcion = itemUsuarioTarea.Descripcion;
                            usuarioTarea.Tarea.FechaVencimiento = itemUsuarioTarea.FechaVencimiento;
                            usuarioTarea.Tarea.Importante = itemUsuarioTarea.Importante;
                            usuarioTarea.Tarea.Estado = new Estado();
                            usuarioTarea.Tarea.Estado.IdEstado = itemUsuarioTarea.IdEstado;

                            list.Add(usuarioTarea);
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

        public static UsuarioTarea GetById(int idUsuario, int idTarea, DataSourceProvider myConnection)
        {
            UsuarioTarea usuarioTarea = new UsuarioTarea();
            try
            {
                string connection = myConnection.GetConnectionString();
                var optionsBuilder = new DbContextOptionsBuilder<DL.HBazanLaudexContext>();
                optionsBuilder.UseSqlServer(connection);

                using (DL.HBazanLaudexContext context = new DL.HBazanLaudexContext(optionsBuilder.Options))
                {
                    var SP = (from objUsuarioTareas in context.UsuarioTareas
                              join objUsuario in context.Usuarios on objUsuarioTareas.IdUsuario equals objUsuario.IdUsuario
                              join objTarea in context.Tareas on objUsuarioTareas.IdTarea equals objTarea.IdTarea
                              where objUsuarioTareas.IdUsuario == idUsuario && objTarea.IdTarea == idTarea
                              select new
                              {
                                  IdUsuarioTareas = objUsuarioTareas.IdUsuarioTarea,
                                  IdUsuario = objUsuario.IdUsuario,
                                  Username = objUsuario.Username,
                                  IdTarea = objTarea.IdTarea,
                                  Titulo = objTarea.Titulo,
                                  Descripcion = objTarea.Descripcion,
                                  FechaVencimiento = objTarea.FechaVencimiento,
                                  Importante = objTarea.Importante,
                                  IdEstado = objTarea.IdEstado
                              }).SingleOrDefault();


                    if (SP != null)
                    {
                        usuarioTarea.IdUsuarioTarea = SP.IdUsuarioTareas;
                        usuarioTarea.Usuario = new Usuario();
                        usuarioTarea.Usuario.IdUsuario = (int)SP.IdUsuario;
                        usuarioTarea.Usuario.Username = SP.Username;
                        usuarioTarea.Tarea = new Tarea();
                        usuarioTarea.Tarea.IdTarea = SP.IdTarea;
                        usuarioTarea.Tarea.Titulo = SP.Titulo;
                        usuarioTarea.Tarea.Descripcion = SP.Descripcion;
                        usuarioTarea.Tarea.FechaVencimiento = SP.FechaVencimiento;
                        usuarioTarea.Tarea.Importante = SP.Importante;
                        usuarioTarea.Tarea.Estado = new Estado();
                        usuarioTarea.Tarea.Estado.IdEstado = SP.IdEstado;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }

            return usuarioTarea;
        }
    }
}
