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
    }
}