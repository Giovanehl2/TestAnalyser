using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class AlunoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarAluno(Aluno aluno)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarAlunoMatricula(aluno.Matricula) == null)
            {
                ctx.Alunos.Add(aluno);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Aluno BuscarAlunoId(int id)
        {
             return ctx.Alunos.Find(id);
        }


        public static Aluno BuscarAlunoMatricula(int matricula )
        {
            return ctx.Alunos.Where(p => p.Matricula.Equals(matricula)).FirstOrDefault();
        }
    }
}