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
            if (BuscarProvaId(prova.ProvaId) == null)
            {
                ctx.Provas.Add(prova);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Prova BuscarProvaId(int id)
        {
            return ctx.Provas.Find(id);
        }
        public static Prova BuscarPorTituloProva(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }

        public static Prova VerificarQuestaoCadastrada(int id)
        {
            /*procura nas provas se existe alguma questão com o id pesquisado*/
            return ctx.Provas.Include(x => x.Questoes).Where(y => y.Questoes.Any(z => z.QuestaoId == id)).FirstOrDefault();

        }

        public static List<Prova> BuscarPorStatus(int status)
        {
            return ctx.Provas.Where(p => p.StatusProva.Equals(status)).ToList();
        }
    }
}