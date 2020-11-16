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
            switch (usr.TipoUsr)
            {
                case 1:
                    if (usr.Aluno != null)
                    {
                        usr.Admin = null;
                        usr.Professor = null;
                    }

                    break;
                case 2:
                    if (usr.Professor != null)
                        usr.Admin = null;
                        usr.Aluno = null;
                    break;
                case 3:
                    if (usr.Admin != null)
                    {
                        usr.Aluno = null;
                        usr.Professor = null;
                    }

                    break;
            }

            usr.Senha = Utilitarios.HashPassword(usr.Senha);
            ctx.Entry(usr).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static Usuario BuscarUsuarioId(int id)
        {
            return ctx.Usuarios.Find(id);
        }
        public static Usuario ValidarPrimeiroAcesso(string login, string cpf)
        {
            Usuario usr = new Usuario();

            usr = ctx.Usuarios.Include("Aluno").Include("Admin").Include("Professor").Where(x => x.Login.Equals(login) && x.Senha == null).FirstOrDefault();
            if(usr != null)
            {
                switch (usr.TipoUsr)
                {
                    case 1:
                        if (usr.Aluno != null && usr.Aluno.CPF.Equals(cpf))
                            return usr;
                        break;
                    case 2:
                        if (usr.Professor != null && usr.Professor.CPF.Equals(cpf))
                            return usr;
                        break;
                    case 3:
                        if (usr.Admin != null && usr.Admin.CPF.Equals(cpf))
                            return usr;
                        break;
                }

            }

            return null;
        }
        public static bool ValidaLogin(Usuario usuario)
        {
            Usuario usr = ctx.Usuarios.Include("Aluno").Include("Admin").Include("Professor").Where(x => x.Login.Equals(usuario.Login)).FirstOrDefault();
            
            if (Utilitarios.ValidatePassword(usuario.Senha, usr.Senha))
            {
                return true;
            }
            return false;
        }
        public static int BuscarRolesUsr(string login)
        {
            return ctx.Usuarios.Where(p => p.Login == login).Select(z => z.TipoUsr).FirstOrDefault();

        }
        public static Usuario BuscarUsuarioPorLogin(String login)
        {
            return ctx.Usuarios.Include("Aluno").Include("Admin").Include("Professor").Where(x => x.Login.Equals(login)).FirstOrDefault();
        }
    }
}