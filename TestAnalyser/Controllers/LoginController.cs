using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Login(FormCollection collection)
        {
            string NomeUser = collection["inputUsuario"].Trim();
            int NA = 0;
            if (NomeUser == "Professor")
            {
                NA = 2;
            }
            else if (NomeUser == "Aluno")
            {
                NA = 1;
            }
            else
            {
                NA = 3;
            }
            //A view passa os valores preenchidos, depois verificar se são validos com o WebService e retornar para a tela inicial...

            Session["NivelAcesso"] = NA;
            Session["NomeUsuario"] = NomeUser;
            return Json(new { status = "success" });
        }

        public ActionResult Logoff()
        {
            string culture = Session["culture"].ToString();
            Session.Clear();

            Session.RemoveAll();

            Session["culture"] = culture;

            return RedirectToAction("Login");

        }
    }
}