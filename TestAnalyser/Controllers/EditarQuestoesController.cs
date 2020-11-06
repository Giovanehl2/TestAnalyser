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
            var QQ = TempData["objquestao"];
            return View(QQ);
        }
        public ActionResult SimplesEscolha()
        {
            var QQ = TempData["objquestao"];
            return View(QQ);
        }
        public ActionResult MultiplaEscolha()
        {
            var QQ = TempData["objquestao"];
            return View(QQ);
        }
        public ActionResult VerdadeiroFalso()
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

        public ActionResult SalvarQuestaoEdit(Questao questao/*, List<int?> AltCorreto*/)
        {
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";
            questao.Disciplina = DisciplinaDAO.BuscarPorNome(questao.Disciplina.Nome);
            //int count = 1;
            //foreach (var item in questao.Alternativas)
            //{
            //    if (AltCorreto.Contains(count))
            //    {
            //        item.correto = 1;
            //    }
            //    else
            //    {
            //        item.correto = 0;
            //    }
            //    count++;
            //}
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

//Projetos Futuros -> Aplicar a função de alterar as alternativas corretas pelo editar...