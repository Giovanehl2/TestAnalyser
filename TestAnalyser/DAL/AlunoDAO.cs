using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class AlunoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarAluno(Aluno usr)
        {
            ctx.Alunos.Add(usr);
            ctx.SaveChanges();
            return true;
        }

        public static bool EditarAluno(Aluno usr)
        {
            ctx.Entry(usr).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Aluno BuscarAlunoId(int id)
        {
            return ctx.Alunos.Find(id);
        }

        public static Aluno BuscarAlunoPorMatricula(int matricula)
        {
            return ctx.Alunos.Include("Turmas").Where(x => x.Matricula == matricula).FirstOrDefault();
        }

    }
}