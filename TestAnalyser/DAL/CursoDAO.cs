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
        public static List<Curso> listarCursos()
        {
            return ctx.Cursos.Include("Disciplinas").Include("Turmas").ToList();

        }
        public static List<Curso> listarCursosPorAluno(int idAluno)
        {
            //parece que ta errado esta trazendo todos precisa fazer ajustes
            return ctx.Cursos.Include("Disciplinas").Include("Turmas").Where(x => x.Turmas.Any(y=> y.Alunos.Any(z=> z.AlunoId == idAluno))).ToList();

        }
        public static List<Curso> listarCursosPorProfessor(int idProfessor)
        {
            //parece que ta errado esta trazendo todos precisa fazer ajustes
            return ctx.Cursos.Include("Disciplinas").Include("Turmas").Where(x=> x.Disciplinas.Any(u => u.Professores.Any(p => p.ProfessorId == idProfessor))).ToList();

        }
        public static Curso BuscarPorNome(string nome)
        {
            return ctx.Cursos.Include("Disciplinas").Include("Turmas").Where(p => p.NomeCurso.Equals(nome)).FirstOrDefault();
        }

        public static List<Curso> BuscarPorProfessor(int id)
        {
            List<Curso> cursos = ctx.Cursos.Include("Disciplinas").Include("Turmas").Where(p => p.Disciplinas.Any(t => t.Professores.Any(x => x.ProfessorId == id))).ToList();
            return cursos;
        }
    }
}