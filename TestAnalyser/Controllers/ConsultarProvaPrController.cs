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
    public class ConsultarProvaPrController : Controller
    {
        private static int cursoId = 0;
        private static int disciplinaId = 0;
        // GET: ConsultarProvaPr
        public ActionResult ConsultarProvaPr(List<Prova> provas)
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
            ViewBag.Provas = TempData["provas"];
            TempData.Keep();
            return View(new Prova());
        }

        public ActionResult CorrigirProva()
        {
            return View();
        }

        public string ConsultarDisciplina(int id)
        {
            cursoId = id;
            var dict = new Dictionary<int, string>();

            foreach (var item in DisciplinaDAO.BuscarPorProfessor(Convert.ToInt32(Session["IdUsr"])))
            {
                foreach (var curso in item.Cursos)
                {
                    if (curso.CursoId == id)
                    {
                        dict.Add(item.DisciplinaId, item.Nome);
                    }
                }
            }

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

            return output;
        }
        public string BuscarTurmas(int id)
        {
            disciplinaId = id;
            var dict = new Dictionary<int, string>();
            Curso curso = CursoDAO.BuscarCursoId(id);
            if(curso != null)
            {
                foreach (var turma in curso.Turmas)
                {
                    dict.Add(turma.TurmaId, turma.NomeTurma);
                }

                var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
                return output;
            }

            return null;
        }

        public ActionResult ConsultarProva( int? Pendentes,  DateTime DataProva, int Curso, int Disciplina, int Turma)
        {
            int Pendente = 0;
            if (Pendentes == null)
            {
                Pendente = 0;
            } else {
                Pendente = 1;
            }
            
            List<Prova> provas = ProvaDAO.BuscarProvasPesquisa(Convert.ToInt32(Session["IdUsr"]), Pendente, DataProva, Curso, Disciplina, Turma);
            TempData["provas"] = provas;
            return RedirectToAction("ConsultarProvaPr", "ConsultarProvaPr");
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