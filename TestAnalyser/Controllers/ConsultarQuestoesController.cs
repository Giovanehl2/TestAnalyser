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
        private static int disciplinaId = 0;
        // GET: ConsultarQuestoes
        public ActionResult ConsultarQuestoes()
        {
            List<Curso> cursos = new List<Curso>();
            Curso curso;
            int id = Convert.ToInt32(Session["IdUsr"]);
            foreach (Disciplina item in DisciplinaDAO.BuscarPorProfessor(id))
            {
                foreach (Curso objCurso in item.Cursos)
                {
                    curso = new Curso();
                    curso.CursoId = objCurso.CursoId;
                    curso.Descricao = objCurso.Descricao;
                    cursos.Add(curso);
                }
            }

            ViewBag.Cursos = cursos.Distinct();
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

        public JsonResult BuscarAssuntos(string id)
        {
            var temp = QuestaoDAO.BuscarAssuntos(id);
            return Json(temp.Distinct());
        }

        public ActionResult ConsultarQuestoesBtn(int Curso, string Disciplina, string Assunto)
        {
            List<Questao> questoes = new List<Questao>();
            //if (Disciplina.Equals("Selecionar"))
            //{
            //    questoes = QuestaoDAO.BuscarPorCurso(Curso);
            //    TempData["questoes"] = questoes;
            //}
            //else 
            if (Assunto.Equals("Selecionar"))
            {
                questoes = QuestaoDAO.BuscarPorDisciplina(Disciplina);
                TempData["questoes"] = questoes;
            }
            else
            {
                questoes = QuestaoDAO.BuscarPorAssunto(Assunto);
                TempData["questoes"] = questoes;
            }

            return RedirectToAction("ConsultarQuestoes", "ConsultarQuestoes");
        }


    }
}