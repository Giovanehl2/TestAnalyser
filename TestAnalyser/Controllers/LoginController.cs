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
        // GET: Login
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
            string hash = Utilitarios.HashPassword("250389");

            bool deu = Utilitarios.ValidatePassword("250389", hash);

            if (deu)
                Console.WriteLine("deu");


            //Validar o Login e Senha digitados na View
            var result = UsuarioDAO.ValidaLogin(usuario);
            if (result != null)
            {
                //Verificar se é o primeiro login, caso sim enviar para a tela de criar uma senha e confirmar dados.
                if (usuario.Senha == null)
                {
                    return RedirectToAction("ConfirmarLogin", "Login");
                }

                //Pegando o nivel de acesso do usuario para mostrar as telas corretas. (NA 1 = aluno, NA 2 = professor, NA 3 = Admin)

                FormsAuthentication.SetAuthCookie(usuario.Login, false);


                int NA = 0;
                string NomeUser = "";
                if (result.TipoUsr == 1)
                {
                    NA = 1;
                    NomeUser = result.Aluno.Nome;
                }else if (result.TipoUsr == 2)
                {
                    NA = 2;
                    NomeUser = result.Professor.Nome;
                }else if (result.TipoUsr == 3)
                {
                    NA = 3;
                    NomeUser = result.Admin.Nome;
                }

                Session["NivelAcesso"] = NA;
                Session["NomeUsuario"] = NomeUser;
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


            var usuario = new Usuario();
            usuario.Login = Login;
            usuario.Senha = null;
            usuario = UsuarioDAO.ValidaLogin(usuario);

            if (usuario.TipoUsr == 1)
            {
                if (usuario.Aluno.CPF.Equals(CPF))
                {
                    UsuarioDAO.SalvarNovoLogin(usuario, Senha);
                }
            } else if (usuario.TipoUsr == 2)
            {
                if (usuario.Professor.CPF.Equals(CPF))
                {
                    UsuarioDAO.SalvarNovoLogin(usuario, Senha);
                }
            }
            else
            {
                if (usuario.Admin.CPF.Equals(CPF))
                {
                    UsuarioDAO.SalvarNovoLogin(usuario, Senha);
                }
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