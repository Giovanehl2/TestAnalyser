using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class CadastrarQuestoesController : Controller
    {
        // GET: CadastrarQuestoes
        //ActionResult para apresetar a tela de cadastro de questões...
        public ActionResult CadastrarQuestoes()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CadastrarQuestoes(Person person)
        {
            return View(person);
        }

        //Metodo para cadastrar a questão de tipo especifico no banco...
        [HttpPost]
        public ActionResult CadastrarSE()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarME()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarVF()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarDT()
        {
            return View();
        }
    }
}