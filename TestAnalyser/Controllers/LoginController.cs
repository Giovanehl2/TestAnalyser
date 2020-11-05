using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestAnalyser.DAL;
using TestAnalyser.Model;
using TestAnalyser.Utils;

namespace TestAnalyser.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmarLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Login,Senha")] Usuario usuario)
        {
            //ApiIntegracaoController.Importar(null);
            //Validar o Login e Senha digitados na View
            var result = UsuarioDAO.BuscarUsuarioPorLogin(usuario.Login);
            if (result != null)
            {
                //Verificar se é o primeiro login, caso sim enviar para a tela de criar uma senha e confirmar dados.
                if (usuario.Senha == null)
                {
                    return RedirectToAction("ConfirmarLogin", "Login");
                }
                else if (UsuarioDAO.ValidaLogin(result))
                {
                    ModelState.AddModelError("", "Usuário ou Senha incorreto!");
                    return View();
                }
                //Pegando o nivel de acesso do usuario para mostrar as telas corretas. (NA 1 = aluno, NA 2 = professor, NA 3 = Admin)
                if (Utilitarios.ValidatePassword(usuario.Senha, result.Senha))
                FormsAuthentication.SetAuthCookie(usuario.Login, false);


                int NA = 0;
                string NomeUser = "";
                int IdUsr = 0;
                if (result.TipoUsr == 1)
                {
                    NA = 1;
                    NomeUser = result.Aluno.Nome;
                    IdUsr = result.Aluno.AlunoId;
                }
                else if (result.TipoUsr == 2)
                {
                    NA = 2;
                    NomeUser = result.Professor.Nome;
                    IdUsr = result.Professor.ProfessorId;
                }
                else if (result.TipoUsr == 3)
                {
                    NA = 3;
                    NomeUser = result.Admin.Nome;
                    IdUsr = result.Admin.AdminId;
                }

                Session["NivelAcesso"] = NA;
                Session["NomeUsuario"] = NomeUser;
                Session["IdUsr"] = IdUsr;
                return RedirectToAction("TelaInicial", "TelaInicial");
            }
            else 
            {
                ModelState.AddModelError("", "Usuário ou Senha incorreto!");
                return View();
            }
        }

        public ActionResult ConfirmarLogin(string CPF, string Login, string Senha)
        {

            Usuario usuario = new Usuario();
            usuario = UsuarioDAO.ValidarPrimeiroAcesso(Login,CPF);
            if (usuario != null)
            {
                usuario.Senha = Senha;
                UsuarioDAO.EditarUsuario(usuario);
            }
            else
            {
                ModelState.AddModelError("", "Dados inconsistentes!");
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}