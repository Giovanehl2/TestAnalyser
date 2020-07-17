using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return ctx.Usuarios.FirstOrDefault(x => x.Login.Equals(usuario.Login) && x.Senha.Equals(usuario.Senha));
        }
    }
}