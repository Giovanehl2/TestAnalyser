using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class AdminDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static bool CadastrarAdmin(Admin admin)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarAdminMatricula(admin.Matricula) == null)
            {
                ctx.Admins.Add(admin);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Admin BuscarAdminId(int id)
        {
            return ctx.Admins.Find(id);
        }


        public static Admin BuscarAdminMatricula(int matricula)
        {
            return ctx.Admins.Where(p => p.Matricula.Equals(matricula)).FirstOrDefault();
        }

        public static bool SalvarConfiguracoes(Configuracao configuracao)
        {
            if(BuscarConfiguracoes() == null)
            {
                ctx.Configuracoes.Add(configuracao);
                ctx.SaveChanges();
                return true;
            }
            else
            {
                ctx.Entry(configuracao).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            
        }

        public static Configuracao BuscarConfiguracoes()
        {
            return ctx.Configuracoes.Where(p => p.ConfiguracaoId == 1).FirstOrDefault();
        }

        public static Configuracao MostrarConfigsTela()
        {
            return ctx.Configuracoes.Where(p => p.ConfiguracaoId == 1).FirstOrDefault();
        }

        //1) Verificar como mostrar na tela os dados ja salvos da instituição. (como enviar o objeto para os campos)
        //2) OS campos de data estão como String, depois basta converte-los para DateTime pelo codigo se necessario.
    }
}