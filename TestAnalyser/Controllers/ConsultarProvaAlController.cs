using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{

    public class ConsultarProvaAlController : Controller
    {
        private static int cursoId = 0;
        // GET: ConsultarProvaAl
        public ActionResult ConsultarProvaAl()
        {
            ViewBag.Cursos = ViewBag.Cursos = CursoDAO.listarCursosPorAluno(Convert.ToInt32(Session["IdUsr"])); ;
            ViewBag.Provas = TempData["provas"];
            TempData.Keep();
            return View(new Prova());
        }
        public ActionResult ConsultarProva(int? Pendentes, DateTime DataIni, DateTime DataFim, int Curso, string Disciplina, int Turma)
        {
            int Pendente;
            if (Pendentes == null)
            {
                Pendente = 0;
            } else {
                Pendente = 1;
            }
            var disciplinaId = DisciplinaDAO.BuscarPorNome(Disciplina).DisciplinaId;
            List<Prova> provas = ProvaDAO.BuscarProvasAluno(Convert.ToInt32(Session["IdUsr"]), Pendente, DataIni, DataFim, Curso, disciplinaId, Turma);
            TempData["provas"] = provas;
            return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
        }

        public ActionResult RealizarProva(int provaID)
        {
            //VERIFICAR SE O ALUNO JA FEZ ESSA PROVA... FAZER BUSCA NA TABELA E VER SE JA TEM DATA DE RESPOSTA...

            Prova prova = ProvaDAO.BuscarProvaId(provaID);
            return View(prova);
        }

        //metodos de consulta
        public string ConsultarDisciplina(int id)
        {
            cursoId = id;
            var dict = new List<string>();

            foreach (var item in DisciplinaDAO.BuscarPorAluno(Convert.ToInt32(Session["IdUsr"]), cursoId))
            {
                foreach (var curso in item.Cursos)
                {
                    if (curso.CursoId == id)
                    {
                        dict.Add(item.Nome);
                    }
                }
            }

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

            return output;
        }

        public string BuscarTurmas(string nome)
        {
            var dict = new Dictionary<int, string>();
            List<Turma> turmas = TurmaDAO.BuscarTurmaDisciplinaNome(nome);
            foreach (var item in turmas)
            {
                dict.Add(item.TurmaId, item.NomeTurma);
            }

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
            return output;
        }

        public ActionResult VisualizarProva(int idProva)
        {
            ViewBag.RespostasAluno = ProvaDAO.BuscarRespostasPorAluno(Convert.ToInt32(Session["IdUsr"]), idProva);

            return View(new Prova());
        }

        public ActionResult FinalizarProva() {

            //TempData["provas"] = provas;   //VERIFICAR COM O SAULO, COMO ZERAR O TEMPDATA...
            return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
        }

        //Verificar a Prova e Questão, e validar a alternativa marcada com a correta no DB.
        public void SalvarQuestaoSE(int QuestaoID, int ProvaID, int AlternativaID)
        {
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            //Tabela RespostasAlunos, provaID x, QuestãoID x, AlunoID x
            RespostasAluno Questao = RespostasAlunoDAO.BuscarProvaQuestaoAluno(QuestaoID, ProvaID, AlunoID);

            double notamax = ProvaDAO.BuscarValorNotamax(ProvaID, QuestaoID);
            foreach (Alternativa item2 in Questao.Questao.Alternativas) {
                if (item2.AlternativaId == AlternativaID && item2.correto == 1)
                    { Questao.NotaAluno = notamax; }
            }
            Questao.SituacaoCorrecao = 1;
            Questao.DataHoraInicio = DateTime.Now;

            RespostasAlunoDAO.Editar(Questao);
        }

        public void SalvarQuestaoMEVF(int QuestaoID, int ProvaID, List<int> AlternativaID)
        {
            
        }

        public void SalvarQuestaoDS(int QuestaoID, int ProvaID, string Resposta)
        {
            
        }
    }
}