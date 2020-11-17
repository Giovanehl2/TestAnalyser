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
        private static List<RespostasAluno> todosGabaritos = new List<RespostasAluno>();
        private static List<RespostasAluno> CorrigirGabarito= new List<RespostasAluno>();
        private static List<RespostasAluno> CorrigirAlunoEspecifico = new List<RespostasAluno>();

        // GET: ConsultarProvaPr
        public ActionResult ConsultarProvaPr()
        {
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
            return View(prova);
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
        public void RecuperaGabaritosAlunos(int ProvaId)
        {
            List<string> status = new List<string>();
            prova = ProvaDAO.BuscarProvaId(ProvaId);
            List<double> notas = new List<double>();

            List<RespostasAluno> result = prova.RespostasAlunos.ToList().GroupBy(elem => elem.Aluno.AlunoId).Select(g => g.First()).ToList();

            foreach (var respostas in result)
            {
                AlunoNota alunoNota =  AlunoNotaDAO.BuscarAlunoNota(respostas.Aluno.AlunoId, prova.ProvaId);
                if(alunoNota != null)
                {
                    notas.Add(alunoNota.NotaTotal);
                }
                else
                {
                    notas.Add(0);
                }

                List<RespostasAluno> resps = new List<RespostasAluno>();
                resps = respostas.Prova.RespostasAlunos.Where(x => x.RespostaDiscursiva != null && x.Aluno.AlunoId == respostas.Aluno.AlunoId).ToList();
                if (resps.Count != 0)
                {
                    foreach (var item in resps)
                    {
                        if (item.SituacaoCorrecao == 1 || item.SituacaoCorrecao == 3 || item.SituacaoCorrecao == 4)
                        {
                            status.Add("Corrigido");
                        }else if (item.SituacaoCorrecao == 0)
                        {
                            status.Add("Processando");
                        }else if (item.SituacaoCorrecao == 2 || item.SituacaoCorrecao == 5)
                        {
                            status.Add("Corrigir Manual");
                        }else
                        {
                            status.Add("Erro!");
                        }
                    }
                }
                else
                {
                    status.Add("Não Realizado");
                }
                
            }

            ViewBag.StatusDSProva = status;
            ViewBag.NotaDaProva = notas;
            ViewBag.RespostasAluno = result;

        }
        //tela para filtrar as provas a serem corrigidas
        public ActionResult OpcoesCorrecao(int? idProva)
        {

            if (idProva != null)
            {
                ProvaId = Convert.ToInt32(idProva);
                RecuperaGabaritosAlunos(ProvaId);
            }
            else
            {
                RecuperaGabaritosAlunos(prova.ProvaId);
            }
      
            return View(ProvaDAO.BuscarProvaId(ProvaId));
        }

        //public ActionResult OpcoesCorrecao(int? idProva)
        //{

        //    if (idProva != null)
        //    {
        //        ProvaId = Convert.ToInt32(idProva);
        //    }

        //    if (todosGabaritos?.Any() != true && CorrigirAlunoEspecifico.Count() == 0)
        //    {
        //        prova = ProvaDAO.BuscarProvaId(ProvaId);
        //        List<RespostasAluno> result = new List<RespostasAluno>();
        //        List<RespostasAluno> resultAjst = new List<RespostasAluno>();
        //        result = prova.RespostasAlunos.ToList().GroupBy(elem => elem.Aluno.AlunoId).Select(g => g.First()).ToList();
        //        foreach (var itns in result)
        //        {
        //            if (itns.DataHoraInicio != null)
        //                resultAjst.Add(itns);
        //        }
        //        todosGabaritos = resultAjst.OrderBy(x => x.Aluno.NomeAluno).ToList();

        //        List<string> Notas = new List<string>();
        //        foreach (var item in todosGabaritos)
        //        {
        //            List<RespostasAluno> Resp = ProvaDAO.BuscarRespostasPorAluno(item.Aluno.AlunoId, ProvaId);
        //            double NotaSomada = 0;

        //            foreach (var item2 in Resp)
        //            {
        //                NotaSomada = (NotaSomada + item2.NotaAluno);
        //            }
        //            Notas.Add(Convert.ToString(NotaSomada));
        //        }
        //        ViewBag.NotaDaProva = Notas;
        //    }
        //    else if (CorrigirAlunoEspecifico.Count() != 0)
        //    {
        //        ViewBag.RespostasAluno = CorrigirAlunoEspecifico;
        //        return View(ProvaDAO.BuscarProvaId(ProvaId));
        //    }


        //    ViewBag.RespostasAluno = todosGabaritos;
        //    return View(ProvaDAO.BuscarProvaId(ProvaId));
        //}
        // tela corrigir prova aluno especifico
        public ActionResult CorrigirRespostastoAluno(int idAluno)
        {
            ViewBag.Provas = new Prova();
            ViewBag.RespostasAluno = new List<RespostasAluno>();
            TempData["ListaRespostasAlunos"] = null;
            return View();
        }
        public ActionResult FiltrarConsulta(int? Situacao, int? matriculaAluno)
        {
            int situac = -1;
            int matr = -1;
            if (Situacao != null)
                situac = Convert.ToInt32(Situacao);


            if (matriculaAluno != null)
                matr = Convert.ToInt32(matriculaAluno);

            if (situac != -1 && matr != -1)
            {
                List<RespostasAluno> respostasFiltradas = new List<RespostasAluno>();
                Aluno aluno = AlunoDAO.BuscarAlunoPorMatricula(matr);
                //adiciona apenas do aluno especifico
                if (aluno != null)
                {
                    CorrigirAlunoEspecifico = new List<RespostasAluno>();
                    List<RespostasAluno> result = new List<RespostasAluno>();
                    result = RespostasAlunoDAO.PerguntasParaCorrigir(aluno.AlunoId, situac).ToList().GroupBy(elem => elem.Aluno.AlunoId).Select(g => g.First()).ToList();
                    CorrigirAlunoEspecifico.AddRange(result);
                }
            }
            else
            {
                CorrigirAlunoEspecifico.Clear();
            }
            
            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr");
        }

        public ActionResult CorrigirProvaAlunoEspecifico(int id, int idProva)
        {
            Prova prova = ProvaDAO.BuscarProvaId(idProva);
            CorrigirGabarito = BuscarRespostasPorAluno(id, idProva);
            ViewBag.Prova = prova;
            ViewBag.RespostasAlunoCorrecao = CorrigirGabarito;

            return View(prova);
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
        public ActionResult VoltarOpcoesCorrecao()
        {
            CorrigirAlunoEspecifico.Clear();
            return RedirectToActionPermanent("OpcoesCorrecao", "ConsultarProvaPr");
        }
         public void limpar()
        {
            cursoId = 0;
            ProvaId = 0;
            //prova = new Prova();
            todosGabaritos = new List<RespostasAluno>();
            CorrigirGabarito= new List<RespostasAluno>();

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

            //limpa os objetos para que sejam regerados e populados com os dados atualizados
            prova = new Prova();
            todosGabaritos = new List<RespostasAluno>();
            CorrigirGabarito = new List<RespostasAluno>();
            return RedirectToAction("OpcoesCorrecao", "ConsultarProvaPr");
        }

        public void GerenciarNotaAluno(int alunoId , int provaId)
        {
            
                AlunoNota alunoNota = new AlunoNota();
                alunoNota = AlunoNotaDAO.BuscarAlunoNota(alunoId, provaId);

                foreach (var item in BuscarRespostasPorAluno(alunoId, provaId))
                {
                    alunoNota.NotaTotal += item.NotaAluno;

                }
                alunoNota.Prova = prova;
                alunoNota.Aluno = AlunoDAO.BuscarAlunoId(alunoId);

                if (AlunoNotaDAO.BuscarAlunoNota(alunoNota.Aluno.AlunoId, alunoNota.Prova.ProvaId) == null)
                {
                    AlunoNotaDAO.CadastrarAlunoNota(alunoNota);
                }
                else
                {
                    AlunoNotaDAO.EditarAlunoNota(alunoNota);
                }

        }

        public ActionResult CorrigirTodaProva(int id, int idProva)
        {
            int AlunoID = id;

            List<RespostasAluno> Resp = ProvaDAO.BuscarRespostasPorAluno(AlunoID, idProva);
            double NotaSomada = 0;

            foreach (var item in Resp)
                { NotaSomada = (NotaSomada + item.NotaAluno); }
            TempData["$NotaAluno$"] = NotaSomada.ToString("F");

            ViewBag.Marcadas = RespostasAlunoDAO.BuscarAltsMarcadas(idProva, AlunoID); ;
            Prova prova = ProvaDAO.BuscarProvaId(idProva);
            prova.RespostasAlunos = Resp;
            return View(prova);
        }

        public void SalvarNotaManual(int QuestaoID, int ProvaID, int AlunoID, string Nota)
        {
            double nota = Convert.ToDouble(Nota);
            Prova provatemp = ProvaDAO.BuscarProvaId(ProvaID);

            RespostasAluno questao;

            foreach (RespostasAluno item in RespostasAlunoDAO.RespostasAlunoProvaId(ProvaID))
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
                else
                {
                    questao = item;
                    questao.Prova = provatemp;
                    RespostasAlunoDAO.Editar(questao);
                }
                
            }


         
        }

    }
}