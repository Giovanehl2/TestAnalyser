using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class AlternativaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarAlternativa(Alternativa alt)
        {
            ctx.Alternativas.Add(alt);
            ctx.SaveChanges();
            return true;
        }

        public static bool EditarAlternativa(Alternativa alt)
        {
            ctx.Entry(alt).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Alternativa BuscarAlternativaId(int id)
        {
            return ctx.Alternativas.Find(id);
        }

    }
}