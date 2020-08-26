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
    }
}