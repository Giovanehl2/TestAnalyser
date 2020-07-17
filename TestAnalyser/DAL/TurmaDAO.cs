using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class TurmaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarTurma(Turma turma)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarTurmaNome(turma.NomeTurma) == null)
            {
                ctx.Turmas.Add(turma);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Turma BuscarTurmaId(int id)
        {
            return ctx.Turmas.Find(id);
        }


        public static Turma BuscarTurmaNome(string nome)
        {
            return ctx.Turmas.Where(p => p.NomeTurma.Equals(nome)).FirstOrDefault();
        }
    }
}