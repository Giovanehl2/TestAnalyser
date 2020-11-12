using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class ProfessorDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarProfessor(Professor prof)
        {
            ctx.Professores.Add(prof);
            ctx.SaveChanges();
            return true;
        }

        public static bool EditarProfessor(Professor prof)
        {
            ctx.Entry(prof).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
        public static Professor BuscarProfessorPorId(int id)
        {
            return ctx.Professores.Include("Disciplinas").Include("Provas").Where(x => x.ProfessorId == id).FirstOrDefault();


        }
        public static Professor BuscarProfessorMatricula(int matricula)
        {
            return ctx.Professores.Include("Disciplinas").Include("Provas").Where(x => x.Matricula.Equals(matricula)).FirstOrDefault();


        }

        public static List<Disciplina> BuscarDisciplinasProfessor(int ProfID)
        {
            List<Disciplina> Disc = new List<Disciplina>();
            var Proff = ctx.Professores.Include("Disciplinas")
                .Include("Disciplinas.Turmas")
                .Include("Disciplinas.Turmas.Alunos")
                .Where(x => x.ProfessorId == ProfID).FirstOrDefault();
            foreach (var item in Proff.Disciplinas)
                { Disc.Add(item); }

            //List<Turma> Turm = new List<Turma>();
            //foreach (var item in Disc)
            //{
            //    var turmas = (item.Turmas);
            //    foreach (var item2 in turmas)
            //    {
            //        if (!Turm.Contains(item2))
            //        {
            //            Turm.Add(item2);
            //        }
            //    }
            //}

            return Disc;
        }
    }
}