using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class OpcaoDAO
    {

        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarOpcao(Opcao opcao)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarOpcaoId(opcao.OpcaoId) == null)
            {
                ctx.Opcoes.Add(opcao);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Opcao BuscarOpcaoId(int id)
        {
            return ctx.Opcoes.Find(id);
        }

    }
}