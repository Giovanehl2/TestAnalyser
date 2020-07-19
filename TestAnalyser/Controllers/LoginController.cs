using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestAnalyser.DAL;
using TestAnalyser.Model;

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


        [HttpPost]
        public ActionResult Login([Bind(Include = "Login,Senha")] Usuario usuario)
        {
            if (UsuarioDAO.ValidaLogin(usuario) != null)
            {
                int NA = 0;
                string NomeUser = "";
                if (usuario.Alunos.AlunoId != 0)
                {
                    NA = 1;
                    NomeUser = usuario.Alunos.Nome;
                }else if (usuario.Professor.ProfessorId != 0)
                {
                    NA = 2;
                    NomeUser = usuario.Professor.Nome;
                }else if (usuario.TipoUsr == 1)
                {
                    NA = 3;
                    NomeUser = "Administrador";
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

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}