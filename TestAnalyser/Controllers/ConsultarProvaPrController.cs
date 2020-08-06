using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class ConsultarProvaPrController : Controller
    {
        // GET: ConsultarProvaPr
        public ActionResult ConsultarProvaPr()
        {
            return View();
        }

        public ActionResult CorrigirProva()
        {
            return View();
        }

        public ActionResult VisualizarTodasAsProvas()
        {
            return View();
        }

        public ActionResult CorrigirProvaAlunoEspecifico()
        {
            return View();
        }
        public ActionResult GerarProva()
        {

            return View();
        }

        public static List<Disciplina> Consultar ()
        {
            return null;
        }


    }
}