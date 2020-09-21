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
        private static int ProvaId = 0;
        private static int disciplinaId = 0;
        private static Prova prova = new Prova();

        // GET: ConsultarProvaPr

        // tela para consulta de provas 
        public ActionResult ConsultarProvaPr()
        {
        
            ViewBag.Cursos = ViewBag.Cursos = CursoDAO.listarCursosPorProfessor(Convert.ToInt32(Session["IdUsr"])); ;
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

        //tela para filtrar as provas a serem corrigidas
        public ActionResult OpcoesCorrecao(int? idProva)
        {
            if(idProva != null)
            ProvaId = Convert.ToInt32(idProva);

            ViewBag.RespostasAluno = TempData["respostasFiltradas"];
            return View(ProvaDAO.BuscarProvaId(ProvaId));
        }
        // tela corrigir prova aluno especifico
        public ActionResult CorrigirRespostastoAluno(int idAluno)
        {
            ViewBag.Provas = new Prova();
            ViewBag.RespostasAluno = new List<RespostasAluno>();
            TempData["ListaRespostasAlunos"] = null;
            return View();
        }
        public ActionResult FiltrarConsulta(int Situacao, int matriculaAluno)
        {
            List<RespostasAluno> respostasFiltradas = new List<RespostasAluno>();
            Aluno aluno = AlunoDAO.BuscarAlunoPorMatricula(matriculaAluno);
            //adiciona apenas do aluno especifico
            if (aluno != null)
            {
                  respostasFiltradas.AddRange(RespostasAlunoDAO.PerguntasParaCorrigir(aluno.AlunoId, Situacao));
            }
            else
            {
                //adiciona todos os alunos
                respostasFiltradas.AddRange(RespostasAlunoDAO.PerguntasParaCorrigir(Situacao));
            }
            TempData["respostasFiltradas"] = respostasFiltradas;

            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr");
        }

        public ActionResult CorrigirProvaAlunoEspecifico(int id, int idProva)
        {
            prova = new Prova();
            prova = ProvaDAO.BuscarRespostasPorAluno(id, idProva);

            ViewBag.RespostasAluno = prova.RespostasAlunos; 

            return View(prova);
        }
        public static List<Disciplina> Consultar ()
        {
            return null;
        }

        public ActionResult SalvarCorrecaoProva(List<int> idquestao, List<double> notas, List<int> idSituacao)
        {

            foreach (RespostasAluno obj in prova.RespostasAlunos)
            {
                for (int i = 0; i < idquestao.Count; i++)
                {
                    if (obj.Questao.QuestaoId == idquestao[i])
                    {
                        obj.SituacaoCorrecao = idSituacao[i];
                        obj.NotaAluno = notas[i];
                        //altera item por item
                        
                    }
                    RespostasAlunoDAO.Editar(obj);
                }
            }


            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr");
        }


    }
}