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
        public static bool EditarTurma(Turma turma)
        {
            ctx.Entry(turma).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Turma BuscarTurmaId(int id)
        {
            return ctx.Turmas.Include("Disciplinas").Include("Alunos").Include("Curso").Where(t =>  t.TurmaId == id).FirstOrDefault();
        }
        public static Turma BuscarTurmaNome(string nome)
        {
            return ctx.Turmas.Include("Disciplinas").Include("Alunos").Include("Curso").Where(p => p.NomeTurma.Equals(nome)).FirstOrDefault();
        }
        public static List<Turma> BuscarTurmaDisciplinaNome(string nome)
        {
            return ctx.Turmas.Include("Disciplinas").Include("Alunos").Include("Curso").Where(p => p.Disciplinas.Any(q => q.Nome.Equals(nome))).ToList();
        }
    }
}