using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class AlunoNotaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarAlunoNota(AlunoNota alunoNota)
        {
            ctx.AlunoNotas.Add(alunoNota);
            ctx.SaveChanges();
            return true;
        }

        public static bool EditarAlunoNota(AlunoNota alunoNota)
        {
            ctx.Entry(alunoNota).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static AlunoNota BuscarAlunoNotaId(int id)
        {
            return ctx.AlunoNotas.Find(id);
        }

        public static AlunoNota BuscarAlunoNota(int idAluno, int idProva)
        {
            return ctx.AlunoNotas.Include("Prova").Include("Aluno").Where(x => x.Aluno.AlunoId == idAluno && x.Prova.ProvaId == idProva).FirstOrDefault();
        }

    }
}