using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class EditarQuestoesController : Controller
    {
        // GET: EditarQuestoes
        public ActionResult Dissertativa()
        {
            var QQ = TempData["objquestao"];
            return View(QQ);
        }

        public ActionResult SalvarDissertativa(Questao questao)
        {
            questao.situacao = 1;
            questao.TipoQuestao = 4;
            questao.Disciplina = DisciplinaDAO.BuscarPorNome(questao.Disciplina.Nome);
            QuestaoDAO.SalvarQuestao(questao);

            TempData["$AlertMessage$"] = "Questão Alterada com Sucesso";
            TempData["objquestao"] = questao;
            
            return RedirectToAction("Dissertativa", "EditarQuestoes");
        }
    }
}