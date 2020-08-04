using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class UsuarioDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarUsuario(Usuario usr)
        {
            if (BuscarPorLogin(usr.Login) == null)
            {
                ctx.Usuarios.Add(usr);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool EditarUsuario(Usuario usr)
        {
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
            var result = new Usuario();
            return result = ctx.Usuarios.Include("Aluno").Include("Admin").Include("Professor")
                .Where(x => x.Login.Equals(usuario.Login) && x.Senha.Equals(usuario.Senha)).FirstOrDefault();
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

            ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

    }
}