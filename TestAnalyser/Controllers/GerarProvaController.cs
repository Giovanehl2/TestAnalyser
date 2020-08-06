using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAnalyser.Controllers
{
    [Authorize]
    public class GerarProvaController : Controller
    {
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            return View();
        }

        public ActionResult AdicionarQuestoesNaProva()
        {
            return View();
        }
    }
}