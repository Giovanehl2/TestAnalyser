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
    public class ConsultarProvaAlController : Controller
    {
        private static int cursoId = 0;
        // GET: ConsultarProvaAl
        public ActionResult ConsultarProvaAl()
        {
            ViewBag.Cursos = ViewBag.Cursos = CursoDAO.listarCursosPorAluno(Convert.ToInt32(Session["IdUsr"])); ;
            ViewBag.Provas = TempData["provas"];
            TempData.Keep("provas");
            return View(new Prova());
        }
        public ActionResult ConsultarProva(int? Pendentes, DateTime DataIni, DateTime DataFim, int Curso, string Disciplina, int Turma)
        {
            int Pendente;
            if (Pendentes == null)
            {
                Pendente = 0;
            }
            else
            {
                Pendente = 1;
            }
            var disciplinaId = DisciplinaDAO.BuscarPorNome(Disciplina).DisciplinaId;
            List<Prova> provas = ProvaDAO.BuscarProvasAluno(Convert.ToInt32(Session["IdUsr"]), Pendente, DataIni, DataFim, Curso, disciplinaId, Turma);
            TempData["provas"] = provas;
            return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
        }

        public ActionResult RealizarProva(int provaID)
        {
            //Verificar se o AlunoID ja realizou a prova...
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            if (RespostasAlunoDAO.VerificarSeProvaFeita(provaID, AlunoID))
            {
                TempData["$ProvaJaFeita$"] = "Você já realizou essa prova.";
                return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
            }

            //Se não realizou, enviar para prova.
            TempData.Remove("$ProvaJaFeita$");
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
            //Verificar se o AlunoID ja realizou a prova...
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            if (!RespostasAlunoDAO.VerificarSeProvaFeita(idProva, AlunoID))
            {
                TempData["$ProvaJaFeita$"] = "Só é possivel visualizar a prova depois de concluída.";
                return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
            }

            //ViewBag.RespostasAluno = ProvaDAO.BuscarRespostasPorAluno(Convert.ToInt32(Session["IdUsr"]), idProva);
            List<RespostasAluno> Resp = ProvaDAO.BuscarRespostasPorAluno(Convert.ToInt32(Session["IdUsr"]), idProva);
            double NotaSomada = 0;
            if (ProvaDAO.MostrarNota(idProva))
            {
                foreach (var item in Resp)
                {
                    NotaSomada = (NotaSomada + item.NotaAluno);
                }
                TempData["$NotaAluno$"] = NotaSomada.ToString("F");

            }
            else
            {
                TempData.Remove("$NotaAluno$");
                TempData["$ProvaJaFeita$"] = "Só é possivel visualizar a prova após a data e hora final.";
                return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
            }

            Prova prova = ProvaDAO.BuscarProvaId(idProva);
            return View(prova);
        }

        public ActionResult FinalizarProva()
        {
            TempData.Remove("provas");
            return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
        }

        //Verificar a Prova e Questão, e validar a alternativa marcada com a correta no DB.
        public void SalvarQuestaoSE(int QuestaoID, int ProvaID, int AlternativaID)
        {
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            //Tabela RespostasAlunos, provaID x, QuestãoID x, AlunoID x
            RespostasAluno Questao = RespostasAlunoDAO.BuscarProvaQuestaoAluno(QuestaoID, ProvaID, AlunoID);

            double notamax = ProvaDAO.BuscarValorNotamax(ProvaID, QuestaoID);
            foreach (Alternativa item2 in Questao.Questao.Alternativas)
            {
                if (item2.AlternativaId == AlternativaID)
                {
                    if (item2.correto == 1)
                    {
                        Questao.NotaAluno = notamax;
                        Questao.SituacaoCorrecao = 1;
                    }
                    else
                    {
                        Questao.NotaAluno = 0;
                        Questao.SituacaoCorrecao = 3;
                    }
                    break;
                }
            }
            Questao.DataHoraInicio = DateTime.Now;

            RespostasAlunoDAO.Editar(Questao);
        }

        //Variavel "Parcial" significa se essa questão aceita nota parcial ou total.
        public void SalvarQuestaoMEVF(int QuestaoID, int ProvaID, List<int> AlternativaID, int? Parcial)
        {
            List<int> corretos = new List<int>();
            var MarcCor = 0;
            var MarcInc = 0;
            double Total = 0;
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            RespostasAluno Questao = RespostasAlunoDAO.BuscarProvaQuestaoAluno(QuestaoID, ProvaID, AlunoID);

            //Se o aluno desmarcar todas as alternativas, o valor é automaticamente 0...
            if (AlternativaID == null)
            {
                Questao.SituacaoCorrecao = 3;
                Questao.NotaAluno = 0;
                Questao.DataHoraInicio = DateTime.Now;

                RespostasAlunoDAO.Editar(Questao);
            }
            else
            {
                //if (Parcial == 1) {} else {}  --  Função separada para ativar e desativar o Salvar nota Parcial ou Total.
                //Pegar todas as AlternativasID dessa questão em especifica...
                List<int> AltersQuestao = new List<int>();
                foreach (Alternativa AltsQuest in Questao.Questao.Alternativas)
                {
                    AltersQuestao.Add(AltsQuest.AlternativaId);
                }

                //Inclui na lista "AltsDtQuest" as "AlternativaID" que pertencem a essa questão...
                List<int> AltsDtQuest = new List<int>();
                foreach (var SelAlts in AlternativaID)
                {
                    if (AltersQuestao.Contains(SelAlts))
                    { AltsDtQuest.Add(SelAlts); }
                }

                //Pega as AlternativasID dessa questão, onde as alternativas são "Corretas"...
                foreach (Alternativa item in Questao.Questao.Alternativas)
                {
                    if (item.correto == 1)
                    { corretos.Add(item.AlternativaId); }
                }
                double notamax = ProvaDAO.BuscarValorNotamax(ProvaID, QuestaoID);
                double notaDiv = (notamax / corretos.Count);

                //Separa quantas alternativas marcadas estão corretas e incorretas...
                foreach (int item2 in AltsDtQuest)
                {
                    if (corretos.Contains(item2))
                    { MarcCor++; }
                    else
                    { MarcInc++; }
                }

                //Atribui o valor total da nota do Aluno com base nas alternativas marcadas...
                Total = (notaDiv * MarcCor);
                Total = (Total - ((notaDiv * MarcInc) / 2));
                if (Total < 0) { Total = 0; }

                //Define qual é o status da questão, correto, parcialmente correto ou incorreto...
                if (MarcCor > 0)
                {
                    if (Total == notamax)
                    { Questao.SituacaoCorrecao = 1; }
                    else
                    { Questao.SituacaoCorrecao = 2; }
                }
                else
                {
                    Questao.SituacaoCorrecao = 3;
                }

                Questao.NotaAluno = Total;
                Questao.DataHoraInicio = DateTime.Now;

                RespostasAlunoDAO.Editar(Questao);
            }
        }

        public void SalvarQuestaoDS(int QuestaoID, int ProvaID, string Resposta)
        {
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            RespostasAluno Questao = RespostasAlunoDAO.BuscarProvaQuestaoAluno(QuestaoID, ProvaID, AlunoID);

            Questao.RespostaDiscursiva = Resposta;
            Questao.SituacaoCorrecao = 0;
            Questao.DataHoraInicio = DateTime.Now;

            RespostasAlunoDAO.Editar(Questao);
        }

        public ActionResult SolicitarRevisao(int ProvaID)
        {
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            var count = 0;
            List<RespostasAluno> resps = RespostasAlunoDAO.SolicitarRevisaoProva(ProvaID, AlunoID);
            foreach (var item in resps)
            {
                item.SituacaoCorrecao = 5;
                RespostasAlunoDAO.Editar(resps[count]);
                count++;
            }
            return RedirectToAction("ConsultarProvaAl", "ConsultarProvaAl");
        }

        //Validar alterações nas questões (se não altera nos lugares errados).
        public void SalvarTodasAlternativas(int ProvaID, List<int> AlternativasID, List<int> QuestoesID)
        {
            var count = 0;
            int AlunoID = Convert.ToInt32(Session["IdUsr"]);
            List<RespostasAluno> Respostas = new List<RespostasAluno>();

            Respostas = RespostasAlunoDAO.BuscarRespostasAluno(ProvaID, AlunoID);
            foreach (var item in QuestoesID)
            {
                foreach (var item2 in Respostas)
                {
                    if (item2.Questao.QuestaoId == item)
                    {
                        Alternativa alt = AlternativaDAO.BuscarAlternativaId(AlternativasID[count]);
                        item2.Alternativas.Add(alt);
                        RespostasAlunoDAO.Editar(item2);
                        break;
                    }
                }
                count++;
            }

            //Resposta.Alternativas = AlternativasID;

        }
    }
}