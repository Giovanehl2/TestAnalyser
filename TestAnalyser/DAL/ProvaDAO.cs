using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class ProvaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarProva(Prova prova)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarProvaDuplicada(prova.TituloProva) == null)
            {
                ctx.Provas.Add(prova);
                ctx.SaveChanges();
                ctx.Entry(prova).State = System.Data.Entity.EntityState.Detached;
                return true;
            }
            return false;
        }
        public static bool EditarProva(Prova prova)
        {
            ctx.Entry(prova).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
        public static Prova BuscarProvaId(int id)
        {
            return ctx.Provas.Find(id);
        }
        public static Prova BuscarProvaDuplicada(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }
        public static Prova BuscarPorTituloProva(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }
        //( )
        public static List<Prova> BuscarProvasPesquisa(int matricula, int Pendente, DateTime DataProva, int Curso, int Disciplina, int Turma)
        {
            List<Prova> result = new List<Prova>();

            var resultado = ctx.Provas.Where(x => x.DataProva.Equals(DataProva) && x.StatusProva == Pendente)
                .Include(c => c.Disciplina)
                .Where(c => c.Disciplina.DisciplinaId == Disciplina).Include(p => p.Professor).Where(p=> p.Professor.Matricula == matricula).ToList();
  

            List<Prova>  provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(p => p.StatusProva == Pendente &&  p.DataProva.Equals(DataProva)).ToList();

            foreach (var item in provas)
            {
                foreach (var item2 in item.Disciplina.Turmas)
                {
                    if (item2.TurmaId == Turma)
                    {
                        result.Add(item);
                    }
                }
            }

            return result;

        }
        public static List<Prova> BuscarProvasPorProfessor(int matricula)
        {
            return ctx.Provas.Where(p => p.Professor.Matricula == matricula).ToList();

        }

        public static List<Prova> BuscarPorStatus(int status)
        {
            return ctx.Provas.Where(p => p.StatusProva.Equals(status)).ToList();
        }
    }
}