using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class AlternativaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarAlternativa(Alternativa alternativa)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarAlternativaId(alternativa.AlternativaId) == null)
            {
                ctx.Alternativas.Add(alternativa);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Alternativa BuscarAlternativaId(int id)
        {
            return ctx.Alternativas.Find(id);
        }
        public static Alternativa BuscarAlterantivaPorDesc(string desc)
        {
            return ctx.Alternativas.Where(p => p.DescAlternativa.Equals(desc)).FirstOrDefault();
        }
    }
}