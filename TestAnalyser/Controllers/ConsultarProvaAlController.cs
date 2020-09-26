using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class ConsultarProvaAlController : Controller
    {
        // GET: ConsultarProvaAl
        public ActionResult ConsultarProvaAl()
        {
            return View();
        }

        public ActionResult RealizarProva(int provaID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(provaID);
            return View(prova);
        }
    }
}