using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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
        private static List<RespostasAluno> Filtrados = new List<RespostasAluno>();
        private static List<RespostasAluno> CorrigirAlunoEspecifico = new List<RespostasAluno>();

        // GET: ConsultarProvaPr
        public ActionResult ConsultarProvaPr()
        {
            ConfiguracoesController.ValidarCorrecoesDiscursivas();
            limpar();
            ViewBag.Cursos = ViewBag.Cursos = CursoDAO.listarCursosPorProfessor(Convert.ToInt32(Session["IdUsr"]));

            if (TempData["provas"] == null)
            {
                ViewBag.Provas = ProvaDAO.BuscarPorProfessor(Convert.ToInt32(Session["IdUsr"]));
            }
            else
            {
                ViewBag.Provas = TempData["provas"];
            }

            return View(new Prova());
        }

        public ActionResult VisualizarProva(int ProvaID)
        {
            
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);

            return View(OrdenarObjetosProva(prova));
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

        public ActionResult ConsultarProva(DateTime DataInicio, DateTime DataFim, int Curso, string Disciplina, string NomeTurma)
        {
            List<Prova> provas = new List<Prova>();
            Disciplina disc = new Disciplina();
            if (!Disciplina.IsEmpty() || !Disciplina.Equals(null)) { }
                disc = DisciplinaDAO.BuscarPorNome(Disciplina);

             if (Disciplina.Equals("Selecionar") )
            {
                provas = ProvaDAO.BuscarProvasPesquisa(Convert.ToInt32(Session["IdUsr"]), DataInicio, DataFim, Curso);
                
            }

            else if ( Curso == 0)
            {
                provas = ProvaDAO.BuscarProvasPesquisa(Convert.ToInt32(Session["IdUsr"]), DataInicio, DataFim);
            }

            if ((!Disciplina.Equals("Selecionar")) && NomeTurma.Equals("Selecionar"))
            {
                provas = ProvaDAO.BuscarProvasPesquisa(Convert.ToInt32(Session["IdUsr"]), DataInicio, DataFim, Curso, disc.DisciplinaId);
            }

            if(!NomeTurma.Equals("Selecionar"))
            {
                provas = ProvaDAO.BuscarProvasPesquisa(Convert.ToInt32(Session["IdUsr"]), DataInicio, DataFim, Curso, disc.DisciplinaId, NomeTurma);
            }
            
            TempData["provas"] = provas;
            return RedirectToAction("ConsultarProvaPr", "ConsultarProvaPr");
        }

        public void RecuperaGabaritosAlunos(int ProvaId, int? Matricula, int? Situacao)
        {
            List<string> status = new List<string>();
            List<string> statusSitu = new List<string>();
            List<double> notas = new List<double>();
            List<double> notasSitu = new List<double>();
            List<RespostasAluno> result = new List<RespostasAluno>();
            List<RespostasAluno> resultSitu = new List<RespostasAluno>();
            prova = ProvaDAO.BuscarProvaId(ProvaId);

            if (Matricula != null)
            {
                var matr = Convert.ToInt32(Matricula);
                Aluno aluno = AlunoDAO.BuscarAlunoPorMatricula(matr);
                result = prova.RespostasAlunos.GroupBy(x => x.Aluno.AlunoId == aluno.AlunoId)
                    .Select(g => g.First()).ToList();
                result.Remove(prova.RespostasAlunos.Where(f => f.Aluno.AlunoId != aluno.AlunoId).First());
            }
            else
            {
                result = prova.RespostasAlunos.GroupBy(x => x.Aluno.AlunoId)
                    .Select(g => g.First()).ToList();
            }

            var cont = 0;
            foreach (var respostas in result)
            {
                notas.Add(ClacularNotaTotalAluno(respostas.Aluno.AlunoId, respostas.Prova.ProvaId));

                List<RespostasAluno> resps = new List<RespostasAluno>();
                resps = respostas.Prova.RespostasAlunos.Where(x => x.RespostaDiscursiva != null && x.Aluno.AlunoId == respostas.Aluno.AlunoId).ToList(); 
                status.Add("");
                if (resps.Count != 0)
                {
                    foreach (var item in resps)
                    {
                        if (item.SituacaoCorrecao == 1 || item.SituacaoCorrecao == 3 || item.SituacaoCorrecao == 4)
                        {
                            status[cont] = "Corrigido";
                        }else if (item.SituacaoCorrecao == 0)
                        {
                            status[cont] = "Processando";
                        }else if (item.SituacaoCorrecao == 2 || item.SituacaoCorrecao == 5)
                        {
                            status[cont] = "Corrigir Manual";
                            break;
                        }else
                        {
                            status[cont] = "Erro!";
                        }
                    }
                }
                else
                {
                    status[cont] = "Não Realizado";
                }
                cont++;
            }

            var cc = 0;
            var cc2 = 0;
            foreach (var respostas in result)
            {
                List<RespostasAluno> resps = new List<RespostasAluno>();
                resps = respostas.Prova.RespostasAlunos.Where(x => x.RespostaDiscursiva != null && x.Aluno.AlunoId == respostas.Aluno.AlunoId).ToList();
                if (resps.Count != 0)
                {
                    var contcor = 0;
                    foreach (var item in resps)
                    {
                        switch (Situacao)
                        {
                            case 0:
                                break;
                            case 1:
                                if (item.SituacaoCorrecao == 1 || item.SituacaoCorrecao == 3 || item.SituacaoCorrecao == 4)
                                {
                                    if (contcor == 0)
                                    {
                                        notasSitu.Add(0);
                                        statusSitu.Add("");
                                        resultSitu.Add(item);
                                        statusSitu[cc] = (status[cc2]);
                                        notasSitu[cc] = (notas[cc2]);
                                        cc++;
                                    }
                                    contcor++;
                                }
                                break;
                            case 2:
                                if (item.SituacaoCorrecao == 0)
                                {
                                    notasSitu.Add(0);
                                    statusSitu.Add("");

                                    resultSitu.Add(item);
                                    statusSitu[cc] = (status[cc2]);
                                    notasSitu[cc] = (notas[cc2]);
                                    cc++;
                                }
                                break;
                            case 3:
                                if (item.SituacaoCorrecao == 2 || item.SituacaoCorrecao == 5)
                                {
                                    notasSitu.Add(0);
                                    statusSitu.Add("");

                                    resultSitu.Add(item);
                                    statusSitu[cc] = (status[cc2]);
                                    notasSitu[cc] = (notas[cc2]);
                                    cc++;
                                    return;
                                }
                                break;

                            default:
                                break;
                        }
                        if (item.SituacaoCorrecao == 2 || item.SituacaoCorrecao == 5)
                            { break; }
                        if (item.SituacaoCorrecao == 0)
                            { break; }
                    }
                    
                }
                else if (Situacao == 4)
                {
                    notasSitu.Add(0);
                    statusSitu.Add("");

                    resultSitu.Add(respostas);
                    statusSitu[cc] = (status[cc2]);
                    notasSitu[cc] = (notas[cc2]);
                    cc++;
                }
                cc2++;
            }

            if (Situacao == 1 || Situacao == 2 || Situacao == 3 || Situacao == 4)
            {
                ViewBag.StatusDSProva = statusSitu;
                ViewBag.NotaDaProva = notasSitu;
                ViewBag.RespostasAluno = resultSitu;
            }
            else
            {
                ViewBag.StatusDSProva = status;
                ViewBag.NotaDaProva = notas;
                ViewBag.RespostasAluno = result;
            }
        }
        //tela para filtrar as provas a serem corrigidas
        public ActionResult OpcoesCorrecao(int? idProva, int? Matricula, int? Situ)
        {

            if (idProva != null)
            {
                ProvaId = Convert.ToInt32(idProva);
                RecuperaGabaritosAlunos(ProvaId, Matricula, Situ);
            }
            else
            {
                RecuperaGabaritosAlunos(prova.ProvaId, Matricula, Situ);
            }
      
            return View(prova);
        }

        public ActionResult CorrigirRespostastoAluno(int idAluno)
        {
            ViewBag.Provas = new Prova();
            ViewBag.RespostasAluno = new List<RespostasAluno>();
            TempData["ListaRespostasAlunos"] = null;
            return View();
        }
        public ActionResult FiltrarConsulta(int Situacao, int? matriculaAluno)
        {

            //int situac = -1;
            //int matr = -1;
            //if (Situacao != 0)
            //    situac = Convert.ToInt32(Situacao);


            //if (matriculaAluno != null)
            //    matr = Convert.ToInt32(matriculaAluno);

            //if (situac != -1 && matr != -1)
            //{
            //    List<RespostasAluno> respostasFiltradas = new List<RespostasAluno>();
            //    Aluno aluno = AlunoDAO.BuscarAlunoPorMatricula(matr);
            //    //adiciona apenas do aluno especifico
            //    if (aluno != null)
            //    {
            //        CorrigirAlunoEspecifico = new List<RespostasAluno>();
            //        List<RespostasAluno> result = new List<RespostasAluno>();
            //        result = RespostasAlunoDAO.PerguntasParaCorrigir(aluno.AlunoId, situac).ToList().GroupBy(elem => elem.Aluno.AlunoId).Select(g => g.First()).ToList();
            //        CorrigirAlunoEspecifico.AddRange(result);
            //    }
            //}
            //else
            //{
            //    CorrigirAlunoEspecifico.Clear();
            //}

            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr", new { Matricula = matriculaAluno, Situ = Situacao });
        }

        public ActionResult CorrigirProvaAlunoEspecifico(int id, int idProva)
        {
            Prova prova = ProvaDAO.BuscarProvaId(idProva);
            ViewBag.Prova = OrdenarObjetosProva(prova);
            ViewBag.RespostasAlunoCorrecao = OrdenarObjetosProva(prova).RespostasAlunos;

            return View(OrdenarObjetosProva(prova));
        }
        private static List<RespostasAluno> BuscarRespostasPorAluno(int idAluno, int idProva)
        {
            List<RespostasAluno> result = new List<RespostasAluno>();
            foreach (RespostasAluno item in RespostasAlunoDAO.BuscarRespostasPorProvaId(idProva))
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
        public void Voltar()
        {
            CorrigirAlunoEspecifico.Clear();

        }
         public void limpar()
        {
            cursoId = 0;
            ProvaId = 0;

        }  

        public ActionResult SalvarCorrecaoProva(List<int> idquestao, List<int> idAluno, List<double> notas, List<int> idSituacao)
        {
            Aluno aluno = new Aluno();
            aluno = AlunoDAO.BuscarAlunoId(idAluno[0]);

            foreach (RespostasAluno obj in BuscarRespostasPorAluno(aluno.AlunoId,prova.ProvaId ))
            {
                if (aluno.AlunoId == obj.Aluno.AlunoId)
                {
                    for (int i = 0; i < idquestao.Count; i++)
                    {
                        if (obj.Questao.QuestaoId == idquestao[i])
                        {
                            obj.SituacaoCorrecao = idSituacao[i];
                            obj.NotaAluno = notas[i];
                            //altera item por item
                            RespostasAlunoDAO.Editar(obj);
                        }


                    }
                   
                }
               
            }

            GerenciarNotaAluno(aluno.AlunoId, prova.ProvaId);

            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr");
        }

        public void GerenciarNotaAluno(int alunoId , int provaId)
        {
            Prova provatemp = ProvaDAO.BuscarProvaId(provaId);
            provatemp.RespostasAlunos.Clear();

            List<RespostasAluno> resultado = RespostasAlunoDAO.RespostasAlunoProvaId(provaId);
            //adiciono pois o resultado não traz correto da base de dados
            provatemp.RespostasAlunos = resultado;

            AlunoNota alunoNota = AlunoNotaDAO.BuscarAlunoNota(alunoId, provaId);
            double notaTotal = 0;
            if (alunoNota == null)
            {
                alunoNota = new AlunoNota();
                alunoNota.Aluno = AlunoDAO.BuscarAlunoId(alunoId);
                alunoNota.Prova = provatemp;
            }
                foreach (var item in BuscarRespostasPorAluno(alunoId, provaId))
                {
                   notaTotal += item.NotaAluno;

                }
                alunoNota.NotaTotal = notaTotal;
                if (AlunoNotaDAO.BuscarAlunoNota(alunoNota.Aluno.AlunoId, alunoNota.Prova.ProvaId) == null)
                {
                    AlunoNotaDAO.CadastrarAlunoNota(alunoNota);
                }
                else
                {
                    AlunoNotaDAO.EditarAlunoNota(alunoNota);
                }

        }

        public double ClacularNotaTotalAluno(int alunoId, int provaId)
        {
            double notaTotal = 0;

            foreach (var item in BuscarRespostasPorAluno(alunoId, provaId))
            {
                if (item.NotaAluno > 2 && item.NotaAluno <= 10)
                {
                    notaTotal = notaTotal + (item.NotaAluno / 10);
                }
                else if (item.NotaAluno > 10)
                {
                    notaTotal = notaTotal + (item.NotaAluno / 100);
                }else
                {
                    notaTotal = notaTotal + item.NotaAluno;
                }
                //notaTotal += item.NotaAluno;
                
            }

            return notaTotal;

        }
        public Prova OrdenarObjetosProva(Prova prova)
        {

            List<RespostasAluno> respostasAluno = prova.RespostasAlunos.OrderBy(o => o.RespostasAlunoId).ToList();
            respostasAluno.ForEach(h => {
                // This will order your cars for this person by Make
                h.Alternativas = h.Alternativas.OrderBy(t => t.AlternativaId).ToList();
                h.Questao.Alternativas = h.Questao.Alternativas.OrderBy(t => t.AlternativaId).ToList();
            });
            prova.RespostasAlunos.Clear();
            prova.RespostasAlunos.AddRange(respostasAluno);
            return prova;
        }

        public ActionResult CorrigirTodaProva(int id, int idProva)
        {
            int AlunoID = id;

            List<RespostasAluno> Resp = ProvaDAO.BuscarRespostasPorAluno(AlunoID, idProva);
            double NotaSomada = 0;

            foreach (var item in Resp) 
            {
                if (item.NotaAluno > 3 && item.NotaAluno <= 10)
                {
                    item.NotaAluno = (item.NotaAluno / 10);
                    NotaSomada = NotaSomada + item.NotaAluno;
                }
                else if (item.NotaAluno > 10)
                {
                    item.NotaAluno = (item.NotaAluno / 100);
                    NotaSomada = NotaSomada + item.NotaAluno;
                }
                else
                {
                    NotaSomada = NotaSomada + item.NotaAluno;
                }
                //NotaSomada = (NotaSomada + item.NotaAluno); 
            }
            TempData["$NotaAluno$"] = NotaSomada.ToString("F");

            ViewBag.Marcadas = RespostasAlunoDAO.BuscarAltsMarcadas(idProva, AlunoID); ;
            Prova prova = ProvaDAO.BuscarProvaId(idProva);
            prova.RespostasAlunos = Resp;


            return View(OrdenarObjetosProva(prova));
        }

        public void SalvarNotaManual(int QuestaoID, int ProvaID, int AlunoID, string Nota)
        {
            double nota = Convert.ToDouble(Nota);
            Prova provatemp = ProvaDAO.BuscarProvaId(ProvaID);
            provatemp.RespostasAlunos.Clear();

            RespostasAluno questao;
            List<RespostasAluno> resultado = RespostasAlunoDAO.RespostasAlunoProvaId(ProvaID);
            //adiciono pois o resultado não traz correto da base de dados
            provatemp.RespostasAlunos = resultado;


            foreach (RespostasAluno item in resultado)
            {
                questao = new RespostasAluno();
                if (item.Aluno.AlunoId == AlunoID && item.Questao.QuestaoId == QuestaoID)
                {
                    questao = item;
                    questao.SituacaoCorrecao = 4;
                    questao.NotaAluno = nota;
                    questao.Prova = provatemp;
                    RespostasAlunoDAO.Editar(questao);
                }

                
            }



        }

    }
}