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
        public static Prova provaFixa = new Prova();

        public static List<Disciplina> disciplinas = new List<Disciplina>();
        public static List<Turma> turmas = new List<Turma>();
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            List<string> assuntos = new List<string>();
            //localiza os cursos de acordo com o id do professor
            ViewBag.Turmas = ProfessorDAO.BuscarDisciplinasProfessor(Convert.ToInt32(Session["IdUsr"]));
            ViewBag.Cursos = CursoDAO.listarCursosPorProfessor(Convert.ToInt32(Session["IdUsr"]));
            return View(new Prova());
        }

        public ActionResult Voltar()
        {
            LimparDadosTela();
            return RedirectToAction("GerarProva", "GerarProva");
        }

        private void LimparDadosTela()
        {
            disciplinas = new List<Disciplina>();
            turmas = new List<Turma>();
            provaFixa = new Prova();

        }
        public JsonResult BuscarDisciplina(int id)
        {
            List<string> listagem = new List<string>();
            disciplinas = DisciplinaDAO.ListarDisciplinas();

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

        public JsonResult BuscarTurma(string nome)
        {
            List<string> listagem = new List<string>();
            turmas = TurmaDAO.BuscarTurmaDisciplinaNome(nome);
            foreach (var item in turmas)
            {
                listagem.Add(item.NomeTurma);
            }

            return Json(listagem);
        }
        public JsonResult BuscarAssunto(string nome)
        {
            var temp = QuestaoDAO.BuscarAssuntos(nome);
            return Json(temp.Distinct());
        }

        public JsonResult BuscarTurmas(string id)
        {
            var temp = QuestaoDAO.BuscarAssuntos(id);
            return Json(temp.Distinct());
        }

        public ActionResult SalvarProvaQuestoes(List<int> idquestao, List<double> notas)
        {
            for (int i = 0; i < provaFixa.NotasQuestoes.Count; i++)
            {
                if (provaFixa.NotasQuestoes[i].Questao.QuestaoId == idquestao[i])
                {
                    provaFixa.NotasQuestoes[i].ValorQuestao = notas[i];
                }
            }
            //adiciona o gabarito de todos os alunosk
            GerarGabaritoAluno();

            if (ProvaDAO.CadastrarProva(provaFixa))
            {
                TempData["$ProvaGerada$"] = "Prova criada com sucesso.";

                return RedirectToAction("GerarProva", "GerarProva");
            }

            return RedirectToAction("AdicionarQuestoesNaProva", "GerarProva", provaFixa);
        }
        private void GerarGabaritoAluno()
        {
            Turma turma = TurmaDAO.BuscarTurmaId(provaFixa.Disciplina.DisciplinaId);
            RespostasAluno respostaAluno;
            List<RespostasAluno> listaRespostaAluno = new List<RespostasAluno>();
            List<Aluno> alunos = turma.Alunos;
            foreach (Aluno aluno in alunos)
            {

                foreach (var item in provaFixa.NotasQuestoes)
                {
                    respostaAluno = new RespostasAluno();
                    respostaAluno.Aluno = aluno;
                    respostaAluno.Questao = item.Questao;
                    respostaAluno.Alternativas = new List<Alternativa>();
                    respostaAluno.DataHoraInicio = null;
                    respostaAluno.DataHoraFim = null;
                    listaRespostaAluno.Add(respostaAluno);
                }
            }
            provaFixa.RespostasAlunos.AddRange(listaRespostaAluno);


        }
        public List<DateTime> ajustarHoraFim(DateTime dataIniProva, DateTime horaIniProva, DateTime dataFimProva, DateTime horaFimProva)
        {
            List<DateTime> listaData = new List<DateTime>();
            try
            {
               // 08/11/2020 15:55:00
                //data hora Inicio
                string dataInicio = dataIniProva.ToString().Substring(0, 11);
                dataInicio += horaIniProva.ToString().Substring(11, 8);

                DateTime HoraInicioCorreta = Convert.ToDateTime(dataInicio);
                listaData.Add(HoraInicioCorreta);


                // data hora Fim
                string dataFim = dataFimProva.ToString().Substring(0, 11);
                dataFim += horaFimProva.ToString().Substring(11, 8);

                DateTime HoraFimCorreta = Convert.ToDateTime(dataFim);

                listaData.Add(HoraFimCorreta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
                
            }

            return listaData;

        }

        public ActionResult CadastrarQuestoesProva(Prova prova)
        {
            List<DateTime> listaData = new List<DateTime>();

            listaData = ajustarHoraFim(prova.DataProvaInicio, prova.HoraInicio, prova.DataProvaFim, prova.HoraFim);

            if(listaData != null)
            {
                prova.HoraInicio = listaData[0];
                prova.HoraFim = listaData[1];
            }



            Disciplina disc = DisciplinaDAO.BuscarPorNome(prova.NomeDisciplina);
            List<RespostasAluno> respostaAluno = new List<RespostasAluno>();
            prova.RespostasAlunos = respostaAluno;
            prova.Disciplina = disc;


            //Aplicando os valores da Faixa de correção (gambs)
            prova.ConfigPln.IncorretoInicio = 0;
            prova.ConfigPln.IncorretoFim = prova.InFim;
            prova.ConfigPln.RevisarInicio = (prova.InFim + 1);
            prova.ConfigPln.RevisarFim = prova.ParFim;
            prova.ConfigPln.CorretoInicio = (prova.ParFim + 1);
            prova.ConfigPln.CorretoFim = 100;

            int id = Convert.ToInt32(Session["IdUsr"]);
            prova.Professor = ProfessorDAO.BuscarProfessorPorId(id);
            provaFixa = prova;
            GerarQuestoesProva();

            if (ProvaDAO.BuscarPorTituloProva(prova.TituloProva) == null)
            {

                return RedirectToAction("AdicionarQuestoesNaProva", "GerarProva", provaFixa);

            }


            return RedirectToAction("GerarProva", "GerarProva", provaFixa);



        }
        public string QuantidadeDeQuestoesPorAssunto(List<string> assuntosSelecionados)
        {
            var dict = new Dictionary<int, int>();
            List<Questao> questoesRaw = new List<Questao>();

            foreach (string assunto in assuntosSelecionados)
                questoesRaw.AddRange(QuestaoDAO.BuscarPorAssunto(assunto));

            //1 = Simples Escolha, 2 = Multipla Escolha, 3 = Verdadeiro ou Falso, 4 = Discursiva.

            dict.Add(1,questoesRaw.FindAll(x => x.TipoQuestao == 1).Count());
            dict.Add(2,questoesRaw.FindAll(x => x.TipoQuestao == 2).Count());
            dict.Add(3,questoesRaw.FindAll(x => x.TipoQuestao == 3).Count());
            dict.Add(4,questoesRaw.FindAll(x => x.TipoQuestao == 4).Count());

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

            return output;
        }

        //public JsonResult QuantidadeDeQuestoesPorAssunto(string assuntosSelecionados)
        //{
        //    QuantidadeQuestoes QuantidadeQuestoes = new QuantidadeQuestoes();
        //    List<Questao> questoesRaw = new List<Questao>();
        //    List<int> qtQuestao = new List<int>();

        //        questoesRaw.AddRange(QuestaoDAO.BuscarPorAssunto(assuntosSelecionados));

        //    //1 = Simples Escolha, 2 = Multipla Escolha, 3 = Verdadeiro ou Falso, 4 = Discursiva.
        //    qtQuestao[0] = questoesRaw.FindAll(x => x.TipoQuestao == 1).Count();
        //    qtQuestao[1] = questoesRaw.FindAll(x => x.TipoQuestao == 2).Count();
        //    qtQuestao[2] = questoesRaw.FindAll(x => x.TipoQuestao == 3).Count();
        //    qtQuestao[3] = questoesRaw.FindAll(x => x.TipoQuestao == 4).Count();



        //    return Json(qtQuestao);
        //}
        private void GerarQuestoesProva()
        {


            NotaQuestao notaquestao;
            List<Questao> questoesProva = new List<Questao>();
            questoesProva.AddRange(gerarQuestoes());
            double nota = Math.Round((provaFixa.ValorProva / questoesProva.Count()), 3);
            List<NotaQuestao> ListanotaQuestao = new List<NotaQuestao>();
            foreach (Questao item in questoesProva)
            {
                notaquestao = new NotaQuestao();
                notaquestao.Questao = item;
                notaquestao.ValorQuestao = nota;
                ListanotaQuestao.Add(notaquestao);

            }
            provaFixa.NotasQuestoes = ListanotaQuestao;
        }

        public ActionResult AdicionarQuestoesNaProva()
        {
            ViewBag.NotasQuestoes = provaFixa.NotasQuestoes;
            return View(provaFixa);
        }

        public ActionResult RegerarQuestoes()
        {
            provaFixa.NotasQuestoes = new List<NotaQuestao>();
            GerarQuestoesProva();
            return RedirectToAction("AdicionarQuestoesNaProva", "GerarProva", provaFixa);
        }

        private List<Questao> gerarQuestoes()
        {
            List<Questao> questoesFinal = new List<Questao>();

            List<Questao> verdadeirFalso = new List<Questao>();
            List<Questao> dissertativa = new List<Questao>();
            List<Questao> multiplaEscolha = new List<Questao>();
            List<Questao> simplesEscolha = new List<Questao>();

            List<Questao> questoesRaw = new List<Questao>();

            List<int> QtVerdadeirFalso = new List<int>();
            List<int> QtDissertativa = new List<int>();
            List<int> QtMultiplaEscolha = new List<int>();
            List<int> QtSimplesEscolha = new List<int>();

            Random rnd = new Random();
            int aleatorio = 0;
            int contador = 0;

            //realiza a busca por assuntos na base de dados e coloca em uma listagem
            foreach (string assunto in provaFixa.Assuntos)
            {
                questoesRaw.AddRange(QuestaoDAO.BuscarPorAssunto(assunto));

            }
            //separa cada questão na respectiva lista para organizãção
            foreach (Questao item in questoesRaw)
            {
                if (item.TipoQuestao == 1)
                {
                    simplesEscolha.Add(item);
                }

                if (item.TipoQuestao == 2)
                {
                    multiplaEscolha.Add(item);
                }

                if (item.TipoQuestao == 3)
                {
                    verdadeirFalso.Add(item);
                }

                if (item.TipoQuestao == 4)
                {
                    dissertativa.Add(item);
                }


            }


            //----------------------------------------------//
            for (int i = 0; i < provaFixa.QtMultiplaEscolha; i++)
            {
                contador = 0;

                while (provaFixa.QtMultiplaEscolha > QtMultiplaEscolha.Count())
                {
                    aleatorio = 0;
                    aleatorio = rnd.Next(0, multiplaEscolha.Count);
                    if (!QtMultiplaEscolha.Contains(aleatorio))
                    {
                        QtMultiplaEscolha.Add(aleatorio);
                    }
                    contador++;
                }


            }

            //----------------------------------------------//
            for (int i = 0; i < provaFixa.QtVerdadeirFalso; i++)
            {

                while (provaFixa.QtVerdadeirFalso > QtVerdadeirFalso.Count())
                {
                    aleatorio = 0;
                    aleatorio = rnd.Next(0, verdadeirFalso.Count);
                    if (!QtVerdadeirFalso.Contains(aleatorio))
                    {
                        QtVerdadeirFalso.Add(aleatorio);
                    }
                }

            }


            //----------------------------------------------//
            for (int i = 0; i < provaFixa.QtDissertativa; i++)
            {

                while (provaFixa.QtDissertativa > QtDissertativa.Count())
                {
                    aleatorio = 0;
                    aleatorio = rnd.Next(0, dissertativa.Count);

                    if (QtDissertativa == null || !QtDissertativa.Exists(item => item == aleatorio))
                    {
                        QtDissertativa.Add(aleatorio);
                    }
                }

            }




            //gera as sequencias aleatorias de acordo com cada tipo de questão
            for (int i = 0; i < provaFixa.QtSimplesEscolha; i++)
            {

                while (provaFixa.QtSimplesEscolha > QtSimplesEscolha.Count())
                {
                    aleatorio = 0;
                    aleatorio = rnd.Next(0, simplesEscolha.Count);
                    if (!QtSimplesEscolha.Contains(aleatorio))
                    {
                        QtSimplesEscolha.Add(aleatorio);
                    }
                }

            }


            //adiciona as questões na lista final
            foreach (int item in QtSimplesEscolha)
            {
                questoesFinal.Add(simplesEscolha[item]);
            }

            //adiciona as questões na lista final
            foreach (int item in QtMultiplaEscolha)
            {
                questoesFinal.Add(multiplaEscolha[item]);
            }

            //adiciona as questões na lista final
            foreach (int item in QtVerdadeirFalso)
            {
                questoesFinal.Add(verdadeirFalso[item]);
            }

            //adiciona as questões na lista final
            foreach (int item in QtDissertativa)
            {
                questoesFinal.Add(dissertativa[item]);
            }
            //----------------------------------------------//
            return questoesFinal;

        }
    }
}