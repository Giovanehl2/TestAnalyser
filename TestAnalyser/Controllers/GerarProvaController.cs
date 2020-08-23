using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
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
    public class GerarProvaController : Controller
    {
        public static List<Disciplina> disciplinas = new List<Disciplina>();
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            Prova prova = new Prova();

            List<string> assuntos = new List<string>();
            int id = Convert.ToInt32(Session["IdUsr"]);

            List<Curso> cursos = new List<Curso>();
            Curso curso;
            disciplinas = DisciplinaDAO.BuscarPorProfessor(id);
            foreach (Disciplina item in disciplinas)
            {
                foreach (Curso objCurso in item.Cursos)
                {
                    curso = new Curso();
                    curso.CursoId = objCurso.CursoId;
                    curso.Descricao = objCurso.Descricao;
                    cursos.Add(curso);
                }

            }
            ViewBag.Cursos = cursos;
            return View(new Prova());
        }
        public JsonResult BuscarDisciplina(int id)
        {
            List<string> listagem = new List<string>();

            foreach (var item in disciplinas)
            {
                foreach (var curso in item.Cursos)
                {
                    if (curso.CursoId == id)
                    {
                        listagem.Add(item.Nome);
                    }

                }


            }

            return Json(listagem);
        }

        public JsonResult BuscarTurma(int id)
        {
            List<string> listagem = new List<string>();

            foreach (var item in disciplinas)
            {
                foreach (var curso in item.Cursos)
                {
                    if (curso.CursoId == id)
                    {
                        foreach (Turma turma in curso.Turmas)
                        {
                            listagem.Add(turma.NomeTurma);
                        }
                    
                    }

                }


            }

            return Json(listagem);
        }
        public JsonResult BuscarAssunto(string id)
        {

            var temp = QuestaoDAO.BuscarAssuntos(id);
            return Json(temp.Distinct());
        }

        public JsonResult BuscarTurmas(string id)
        {
            var temp = QuestaoDAO.BuscarAssuntos(id);
            return Json(temp.Distinct());
        }
        public ActionResult AdicionarQuestoesNaProva()
        {
            return View();
        }

        public ActionResult SalvarProva(Prova prova)
        {
            return RedirectToAction("Index", "Investimento");
        }

    }
}