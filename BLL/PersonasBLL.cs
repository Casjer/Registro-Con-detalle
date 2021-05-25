using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Tarea3.DAL;
using Tarea3;
using Tarea3.Models;

namespace Tarea3Ap2_JoseLuis.BLL
{
    public class PersonasBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(Personas personas)
        {
            if (!Existe(personas.PersonaId))
                return Insertar(personas);
            else
                return Modificar(personas);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        public static bool Insertar(Personas personas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Personas.Add(personas);
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
        //——————————————————————————————————————————————[ MODIFICAR ]——————————————————————————————————————————————
        public static bool Modificar(Personas personas)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(personas).State = EntityState.Modified;
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
        //——————————————————————————————————————————————[ ELIMINAR ]——————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var personas = contexto.Personas.Find(id);
                if (personas != null)
                {
                    personas.Visibilidad = false;//Visibilidad en el Sistema.
                    contexto.Personas.Update(personas);//Visibilidad en el Sistema.
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
        //——————————————————————————————————————————————[ BUSCAR ]——————————————————————————————————————————————
        public static Personas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Personas personas;

            try
            {
                personas = contexto.Personas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            #region————————————————————————[ No mostrar si la visibilidad es false ]————————————————————————
            if (personas?.Visibilidad == false) //? para ejecutar el if aun que sea null
            {
                return null;
            }
            #endregion

            return personas;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<Personas> GetList(Expression<Func<Personas, bool>> criterio)
        {
            List<Personas> lista = new List<Personas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Personas.Where(criterio).Where(v => v.Visibilidad == true).ToList();//Visibilidad en el Sistema. (Consulta)
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
        //——————————————————————————————————————————————[ EXISTE ]——————————————————————————————————————————————
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Personas.Any(e => e.PersonaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<Personas> GetPersonas()
        {
            List<Personas> lista = new List<Personas>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Personas.ToList();
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
        //——————————————————————————————————————————————[ Sumar - Balance ]——————————————————————————————————————————————
        public static void SumarBalance(int id, double cantidad)
        {
            Personas personas = Buscar(id);

            personas.Balance += cantidad;

            Modificar(personas);
        }
        //——————————————————————————————————————————————[ Restar - Balance ]——————————————————————————————————————————————
        public static void RestarBalance(int id, double cantidad)
        {
            Personas personas = Buscar(id);

            personas.Balance -= cantidad;

            Modificar(personas);
        }
    }
}