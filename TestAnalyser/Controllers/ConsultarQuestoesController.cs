using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    //Filtro: Curso, Disciplina e Assunto.
    public class ConsultarQuestoesController : Controller
    {
        private static int cursoId = 0;
        //private static int disciplinaId = 0;
        // GET: ConsultarQuestoes
        public ActionResult ConsultarQuestoes()
        {
            int id = Convert.ToInt32(Session["IdUsr"]);
            List<Curso> cursos = CursoDAO.BuscarPorProfessor(id);

            ViewBag.Cursos = cursos;
            ViewBag.Questoes = TempData["questoes"];
            TempData.Keep();
            return View(new Questao());
        }

        public string ConsultarDisciplina(int id)
        {
            cursoId = id;
            var dict = new Dictionary<string, string>();

            foreach (var item in DisciplinaDAO.BuscarPorProfessor(Convert.ToInt32(Session["IdUsr"])))
            {
                foreach (var curso in item.Cursos)
                {
                    if (curso.CursoId == id)
                    {
                        dict.Add(item.Nome, item.Nome);
                    }
                }
            }

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

            return output;
        }

        public JsonResult ConsultarAssuntos(string id)
        {
            var temp = QuestaoDAO.BuscarAssuntos(id);
            return Json(temp.Distinct());
        }

        public ActionResult ConsultarQuestoesBtn(string Disciplina, string Assunto, int? Desativado)
        {
            List<Questao> questoes = new List<Questao>();
            if (Assunto.Equals("Selecionar"))
            {
                questoes = QuestaoDAO.BuscarPorDisciplina(Disciplina, Desativado);
                TempData["questoes"] = questoes;
            }
            else
            {
                questoes = QuestaoDAO.BuscarPorAssunto(Assunto);
                TempData["questoes"] = questoes;
            }

            return RedirectToAction("ConsultarQuestoes", "ConsultarQuestoes");
        }

        public ActionResult ExcluirQuestao(int questaoID)
        {
            QuestaoDAO.ExcDesQuestao(questaoID);
            return RedirectToAction("ConsultarQuestoes", "ConsultarQuestoes");
        }

        public ActionResult EditarQuestao(int questaoID)
        {
            Questao OBJ = QuestaoDAO.BuscarQuestaoId(questaoID);
            TempData["objquestao"] = OBJ;
            return RedirectToAction("Dissertativa", "EditarQuestoes");
        }
    }
}