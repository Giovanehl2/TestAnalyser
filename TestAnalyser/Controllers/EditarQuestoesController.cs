using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    [Authorize]
    public class EditarQuestoesController : Controller
    {
        // GET: EditarQuestoes
        public ActionResult Dissertativa()
        {
            ViewBag.AssuntosQuestao = QuestaoDAO.BuscarAssuntosQuestaoTodos();
            var QQ = TempData["objquestao"];
            return View(QQ);
        }
        public ActionResult SimplesEscolha()
        {
            ViewBag.AssuntosQuestao = QuestaoDAO.BuscarAssuntosQuestaoTodos();
            var QQ = TempData["objquestao"];
            return View(QQ);
        }
        public ActionResult MultiplaEscolha()
        {
            ViewBag.AssuntosQuestao = QuestaoDAO.BuscarAssuntosQuestaoTodos();
            var QQ = TempData["objquestao"];
            return View(QQ);
        }
        public ActionResult VerdadeiroFalso()
        {
            ViewBag.AssuntosQuestao = QuestaoDAO.BuscarAssuntosQuestaoTodos();
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

        public ActionResult SalvarQuestaoEdit(Questao questao, List<bool?> AltCorreto, List<int?> AltCorretoSE)
        {
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";
            questao.Disciplina = DisciplinaDAO.BuscarPorNome(questao.Disciplina.Nome);
            if (AltCorreto != null)
            {
                int count = 0;
                foreach (var item in questao.Alternativas)
                {
                    if (AltCorreto[count] == true)
                    { item.correto = 1; count++; }
                    else
                    { item.correto = 0; }
                    count++;
                }
            }
            else
            {
                int count2 = 1;
                foreach (var item in questao.Alternativas)
                {
                    if (AltCorretoSE[0] == count2)
                    { item.correto = 1; }
                    else
                    { item.correto = 0; }
                    count2++;
                }
            }
            
            QuestaoDAO.SalvarQuestao(questao);

            TempData["$AlertMessage$"] = "Questão Editada com Sucesso";
            TempData["objquestao"] = questao;

            switch (questao.TipoQuestao)
            {
                case 1:
                    return RedirectToAction("SimplesEscolha", "EditarQuestoes");
                case 2:
                    return RedirectToAction("MultiplaEscolha", "EditarQuestoes");
                case 3:
                    return RedirectToAction("VerdadeiroFalso", "EditarQuestoes");
                default:
                    break;
            }
            return RedirectToAction("", "");
        }
    }
}
