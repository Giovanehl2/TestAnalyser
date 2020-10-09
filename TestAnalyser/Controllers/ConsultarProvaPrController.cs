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
        private static Prova prova;
        private static List<RespostasAluno> todosGabaritos = new List<RespostasAluno>();
        private static List<RespostasAluno> CorrigirGabarito= new List<RespostasAluno>();

        // GET: ConsultarProvaPr
        public ActionResult ConsultarProvaPr()
        {
            limpar();
            ViewBag.Cursos = ViewBag.Cursos = CursoDAO.listarCursosPorProfessor(Convert.ToInt32(Session["IdUsr"])); ;
            ViewBag.Provas = TempData["provas"];
            TempData.Keep();
            return View(new Prova());
        }

        public string ConsultarDisciplina(int id)
        {
            cursoId = id;
            var dict = new List<string>(); //Dictionary<int, string>();

            foreach (var item in DisciplinaDAO.BuscarPorProfessor(Convert.ToInt32(Session["IdUsr"])))
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

        public ActionResult ConsultarProva( int? Pendentes,  DateTime DataInicio, DateTime DataFim, int Curso, string Disciplina, int Turma)
        {
            int Pendente = 0;
            if (Pendentes == null)
            {
                Pendente = 0;
            } else {
                Pendente = 1;
            }
            var disciplinaId = DisciplinaDAO.BuscarPorNome(Disciplina).DisciplinaId;
            List<Prova> provas = ProvaDAO.BuscarProvasPesquisa(Convert.ToInt32(Session["IdUsr"]), Pendente, DataInicio, DataFim, Curso, disciplinaId, Turma);
            TempData["provas"] = provas;
            return RedirectToAction("ConsultarProvaPr", "ConsultarProvaPr");
        }

        //tela para filtrar as provas a serem corrigidas
        public ActionResult OpcoesCorrecao(int? idProva)
        {

            if (idProva != null)
            {
                ProvaId = Convert.ToInt32(idProva);
            }

            if (todosGabaritos?.Any() != true) {
                prova = ProvaDAO.BuscarProvaId(ProvaId);
                List<RespostasAluno> result = new List<RespostasAluno>();
                result = prova.RespostasAlunos.ToList().GroupBy(elem => elem.Aluno.AlunoId).Select(g => g.First()).ToList();
                todosGabaritos = result.OrderBy(x => x.Aluno.Nome).ToList();
            }


            ViewBag.RespostasAluno = todosGabaritos;
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
            CorrigirGabarito = BuscarRespostasPorAluno(id, idProva);
            ViewBag.RespostasAlunoCorrecao = CorrigirGabarito;

            return View(prova);
        }
        private static List<RespostasAluno> BuscarRespostasPorAluno(int idAluno, int idProva)
        {
            List<RespostasAluno> result = new List<RespostasAluno>();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == idAluno)
                    result.Add(item);
            }
            return result;
        }

        public static List<Disciplina> Consultar ()
        {
            return null;
        }
        public ActionResult VoltarOpcoesCorrecao()
        {
            return RedirectToActionPermanent("OpcoesCorrecao", "ConsultarProvaPr");
        }
         public void limpar()
        {
            cursoId = 0;
            ProvaId = 0;
            prova = new Prova();
            todosGabaritos = new List<RespostasAluno>();
            CorrigirGabarito= new List<RespostasAluno>();

        }  

        public ActionResult SalvarCorrecaoProva(List<int> idquestao, List<double> notas, List<int> idSituacao)
        {
            Aluno aluno = new Aluno();
            foreach (RespostasAluno obj in CorrigirGabarito)
            {
                if (aluno == null)
                    aluno = obj.Aluno;
     

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
            //verificar funcionalidade
            if(AlunoNotaDAO.BuscarAlunoNota(aluno.AlunoId, ProvaId) == null)
            {
                AlunoNota alunoNota = new AlunoNota();
                foreach (var item in BuscarRespostasPorAluno(aluno.AlunoId, ProvaId))
                {
                    alunoNota.NotaTotal += item.NotaAluno;

                }
                alunoNota.Prova = prova;
                alunoNota.Aluno = aluno;
                AlunoNotaDAO.CadastrarAlunoNota(alunoNota);
            }

            //limpa os objetos para que sejam regerados e populados com os dados atualizados
            prova = new Prova();
            todosGabaritos = new List<RespostasAluno>();
            CorrigirGabarito = new List<RespostasAluno>();
            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr");
        }


    }
}