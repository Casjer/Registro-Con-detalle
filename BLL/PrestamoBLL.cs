using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Tarea3.DAL;
using Tarea3.Models;



namespace Tarea3.BLL
{
    public class PrestamoBLL
    {
        public static bool Guardar(Prestamo prestamo)
        {
            if (!Existe(prestamo.PrestamoId))
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        private static bool Insertar(Prestamo prestamo)
        {
            Contexto context = new Contexto();
            bool paso = false;

            try
            {
                prestamo.Balance = prestamo.Monto;
                context.Prestamo.Add(prestamo);
                paso = context.SaveChanges() > 0;

                Persona persona = PersonaBLL.Buscar(prestamo.PersonaId);
                persona.Balance += prestamo.Monto;
                PersonaBLL.Modificar(persona);

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return paso;
        }
        public static bool Modificar(Prestamo prestamo)
        {
            Contexto context = new Contexto();
            bool paso = false;

            try
            {
                prestamo.Balance = prestamo.Monto;
                Prestamo prestamoAnterior = Buscar(prestamo.PrestamoId);
                float nuevoMonto = (float)(prestamo.Monto - prestamoAnterior.Monto);

                Persona persona = PersonaBLL.Buscar(prestamo.PersonaId);
                persona.Balance += nuevoMonto;
                PersonaBLL.Modificar(persona);

                context.Entry(prestamo).State = EntityState.Modified;
                paso = context.SaveChanges() > 0;

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return paso;
        }
        public static bool Eliminar(int id)
        {
            Contexto context = new Contexto();
            bool paso = false;

            try
            {
                var prestamo = context.Prestamo.Find(id);

                if (prestamo != null)
                {
                    Persona persona = PersonaBLL.Buscar(prestamo.PersonaId);
                    persona.Balance -= prestamo.Monto;
                    PersonaBLL.Modificar(persona);

                    context.Prestamo.Remove(prestamo);
                    paso = context.SaveChanges() > 0;
                }

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return paso;
        }
        public static Prestamo Buscar(int id)
        {
            Contexto context = new Contexto();
            Prestamo prestamo;

            try
            {
                prestamo = context.Prestamo
                    .Where(p => p.PrestamoId == id)
                    .SingleOrDefault();

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return prestamo;
        }
        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool paso = false;

            try
            {
                paso = context.Prestamo.Any(p => p.PrestamoId == id);

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return paso;
        }

        public static List<Prestamo> GetList(Expression<Func<Prestamo, bool>> criterio)
        {
            List<Prestamo> list = new List<Prestamo>();
            Contexto context = new Contexto();

            try
            {
                list = context.Prestamo.Where(criterio).AsNoTracking().ToList();

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return list;
        }

    }
}