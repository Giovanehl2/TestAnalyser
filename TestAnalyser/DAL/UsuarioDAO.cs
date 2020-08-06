using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;
using TestAnalyser.Utils;

namespace TestAnalyser.DAL
{
    public class UsuarioDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarUsuario(Usuario usr)
        {
            if (BuscarUsuarioPorLogin(usr.Login) == null)
            {
                ctx.Usuarios.Add(usr);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditarUsuario(Usuario usr)
        {
            usr.Senha = Utilitarios.HashPassword(usr.Senha);
            ctx.Entry(usr).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Usuario BuscarUsuarioId(int id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static Usuario ValidaLogin(Usuario usuario)
        {


            Usuario usr = ctx.Usuarios.Include("Aluno").Include("Admin").Include("Professor").Where(x => x.Login.Equals(usuario.Login)).FirstOrDefault();
            if(Utilitarios.ValidatePassword(usuario.Senha, usr.Senha))
            {
                return usr;
            }
            return null;
        }
        public static int BuscarRolesUsr(string login)
        {
            return ctx.Usuarios.Where(p => p.Login == login).Select(z => z.TipoUsr).FirstOrDefault();

        }
        public static Usuario BuscarUsuarioPorLogin(String login)
        {
            return ctx.Usuarios.Include("Aluno").Include("Admin").Include("Professor").Where(x => x.Login.Equals(login)).FirstOrDefault();
        }

        public static bool SalvarNovoLogin(Usuario usuario, string Senha)
        {
            //Confirmar CPF e salvar a senha digitada na View.
            usuario.Senha = Senha;

            if (usuario.TipoUsr == 1)
            {
                usuario.Professor = null;
                usuario.Admin = null;
            }
            else if (usuario.TipoUsr == 2)
            {
                usuario.Aluno = null;
                usuario.Admin = null;
            }
            else
            {
                usuario.Aluno = null;
                usuario.Professor = null;
            }
            //fazer ajuste do hash
            ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
        public void BCryptTest()
        {

            const string password = "PASSWORD";
            const int workFactor = 13;

            var start = DateTime.UtcNow;
            var hashed = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
            var end = DateTime.UtcNow;


        }
    }
}