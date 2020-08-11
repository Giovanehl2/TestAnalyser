using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class DisciplinaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarDisciplina(Disciplina disciplina)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarPorNome(disciplina.Nome) == null)
            {
                ctx.Disciplinas.Add(disciplina);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Disciplina BuscarDisciplinaId(int id)
        {
            return ctx.Disciplinas.Find(id);
        }

        public static List<Disciplina> ListarDisciplinas()
        {
            return ctx.Disciplinas.ToList();
        }

        public static bool EditarDisciplina(Disciplina disciplina)
        {
            ctx.Entry(disciplina).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }


        public static Disciplina BuscarPorNome(string nome)
        {
            return ctx.Disciplinas.Include("Turmas").Include("Professores").Include("Cursos").Where(p => p.Nome.Equals(nome)).FirstOrDefault();

        }
    }
}
