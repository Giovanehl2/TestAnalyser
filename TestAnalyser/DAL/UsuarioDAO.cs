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
        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarUsuario(Usuario usr)
        {
            ctx.Usuarios.Add(usr);
            ctx.SaveChanges();
            return true;
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
            return ctx.Usuarios.FirstOrDefault(x => x.Login.Equals(usuario.Login) && x.Senha.Equals(usuario.Senha));
        }

        public static Aluno BuscarAlunoId(int id)
        {
            return ctx.Alunos.Find(id);
        }

        public static Aluno BuscarAlunoMatricula(int matricula)
        {
            return ctx.Alunos.Where(p => p.Matricula.Equals(matricula)).FirstOrDefault();
        }

        public Professor BuscarProfessorId(int id)
        {
            return ctx.Professores.Find(id);
        }

        public static Professor BuscarProfessorMatricula(int matricula)
        {
            return ctx.Professores.Where(p => p.Matricula.Equals(matricula)).FirstOrDefault();
        }
    }
}