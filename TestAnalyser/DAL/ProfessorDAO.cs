using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class ProfessorDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarProfessor(Professor professor)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarProfessorMatricula(professor.Matricula) == null)
            {
                ctx.Professores.Add(professor);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public Professor BuscarProfessorId(int id)
        {
            return ctx.Professores.Find(id);
        }


        public static Professor BuscarProfessorMatricula(int matricula)
        {
            return ctx.Professores.Where(p => p.Matricula.Equals(matricula)).FirstOrDefault();
        }
    }
}