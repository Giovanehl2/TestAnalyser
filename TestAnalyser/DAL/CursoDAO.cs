using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class CursoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarCurso(Curso curso)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarPorNome(curso.NomeCurso) == null)
            {
                ctx.Cursos.Add(curso);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditarCurso(Curso curso)
        {
            ctx.Entry(curso).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Curso BuscarCursoId(int id)
        {
            return ctx.Cursos.Find(id);
        }


        public static Curso BuscarPorNome(string nome)
        {
            return ctx.Cursos.Where(p => p.NomeCurso.Equals(nome)).FirstOrDefault();
        }
    }
}