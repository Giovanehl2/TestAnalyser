using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class CadastrarQuestoesController : Controller
    {
        // GET: CadastrarQuestoes
        //ActionResult para apresetar a tela de cadastro de questões...
        public ActionResult CadastrarQuestoes()
        {
            ViewBag.Disciplinas = DisciplinaDAO.ListarDisciplinas();
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarQuestoes(Questao questao)
        {

            return View(questao);
        }

        //Metodo para cadastrar a questão de tipo especifico no banco...
        [HttpPost]
        public ActionResult CadastrarSE(Questao questao)
        {
            questao.TipoQuestao = 1;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";
            //QuestaoDAO.CadastrarQuestao(questao);

            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
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