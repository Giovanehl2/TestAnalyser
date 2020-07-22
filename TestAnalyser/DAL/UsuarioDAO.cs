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
        public static bool CadastrarUsuario(Usuario usr)
        {

            return false;
        }

        public static Usuario BuscarUsuarioPorEmailSenha(Usuario usuario)
        {
            var result = new Usuario();
            return result = ctx.Usuarios.Include("Alunos").Include("Admin").Include("Professor")
                .Where(x => x.Login.Equals(usuario.Login) && x.Senha.Equals(usuario.Senha)).FirstOrDefault();
            //return result = (from x in ctx.Usuarios where x.Login == usuario.Login && x.Senha == usuario.Senha select x).First();
        }

        public Usuario BuscarUsuarioId(int id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static bool SalvarNovoLogin(Usuario usuario, string Senha)
        {
            //Confirmar CPF e salvar a senha digitada na View.
            usuario.Senha = Senha;

            if (usuario.TipoUsr == 1)
            {
                usuario.Professor = null;
                usuario.Admin = null;
            }else if (usuario.TipoUsr == 2)
            {
                usuario.Alunos = null;
                usuario.Admin = null;
            } else
            {
                usuario.Alunos = null;
                usuario.Professor = null;
            }

            ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
    }
}