using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class OpcaoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarOpcao(Opcao opt)
        {
            ctx.Opcoes.Add(opt);
            ctx.SaveChanges();
            return true;
        }

        public static bool EditarOpcao(Opcao opt)
        {
            ctx.Entry(opt).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Opcao BuscarOpcaoId(int id)
        {
            return ctx.Opcoes.Find(id);
        }

    }
}