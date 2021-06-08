using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Tarea3.DAL;
using Tarea3.Models;

namespace Tarea3.BLL
{
    public class PersonaBLL
    {
        public static bool Guardar(Persona persona)
        {
            if (!Existe(persona.PersonaId))
                return Insertar(persona);
            else
                return Modificar(persona);
        }

        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                found = context.Persona.Any(p => p.PersonaId == id);

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }


        private static bool Insertar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                contexto.Persona.Add(persona);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }



        public static bool Modificar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                contexto.Entry(persona).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {

                var Persona = contexto.Persona.Find(id);

                if (Persona != null)
                {
                    contexto.Persona.Remove(Persona);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Persona Buscar(int id)
        {
            Contexto context = new Contexto();
            Persona persona;

            try
            {
                persona = context.Persona.Find(id);

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return persona;

        }


        public static List<Persona> GetList(Expression<Func<Persona, bool>> criterio)
        {
            List<Persona> lista = new List<Persona>();
            Contexto contexto = new Contexto();
            try
            {

                lista = contexto.Persona.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }




        public static List<Persona> GetPersona()
        {
            List<Persona> lista = new List<Persona>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Persona.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}