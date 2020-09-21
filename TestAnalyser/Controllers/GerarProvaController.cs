﻿using Microsoft.Ajax.Utilities;
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
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            Prova prova = new Prova();

            List<string> assuntos = new List<string>();
            //localiza os cursos de acordo com o id do professor
            ViewBag.Cursos = CursoDAO.listarCursosPorProfessor(Convert.ToInt32(Session["IdUsr"])); ;
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

        public ActionResult SalvarProvaQuestoes(List<int> idquestao, List<double> notas)
        {
                for (int i = 0; i < provaFixa.NotasQuestoes.Count; i++)
                {
                   if( provaFixa.NotasQuestoes[i].Questao.QuestaoId == idquestao[i])
                    {
                    provaFixa.NotasQuestoes[i].ValorQuestao = notas[i];
                    }
                }
                //adiciona o gabarito de todos os alunosk
                GerarGabaritoAluno();

            if (ProvaDAO.CadastrarProva(provaFixa))
            {

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

        public ActionResult CadastrarQuestoesProva(Prova prova)
        {
            if (ModelState.IsValid)
            {
                Disciplina disc = DisciplinaDAO.BuscarPorNome(prova.NomeDisciplina);
                List<RespostasAluno> respostaAluno = new List<RespostasAluno>();
                prova.RespostasAlunos = respostaAluno;
                prova.Disciplina = disc;

                int id = Convert.ToInt32(Session["IdUsr"]);
                prova.Professor = ProfessorDAO.BuscarProfessorPorId(id);
                provaFixa = prova;
                GerarQuestoesProva();

                if (ProvaDAO.BuscarPorTituloProva(prova.TituloProva) == null)
                {
                    
                    return RedirectToAction("AdicionarQuestoesNaProva", "GerarProva", provaFixa);

                }

            }
            ModelState.AddModelError("", "Não foi possivel realizar o cadastro, por favor preencha todos os campos!");
            return RedirectToAction("GerarProva", "GerarProva", provaFixa);



        }
        private void GerarQuestoesProva()
        {
            NotaQuestao notaquestao;
            List<Questao> questoesProva = new List<Questao>();
            questoesProva.AddRange(gerarQuestões());
            double nota = provaFixa.ValorProva / questoesProva.Count();
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

        private List<Questao> gerarQuestões()
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
            //gera as sequencias aleatorias de acordo com cada tipo de questão
            for (int i = 0; i < provaFixa.QtSimplesEscolha; i++)
            {
                QtSimplesEscolha.Add(rnd.Next(0, simplesEscolha.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtSimplesEscolha)
            {
                questoesFinal.Add(simplesEscolha[item]);
            }

            //----------------------------------------------//
            for (int i = 0; i < provaFixa.QtMultiplaEscolha; i++)
            {
                QtMultiplaEscolha.Add(rnd.Next(0, multiplaEscolha.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtMultiplaEscolha)
            {
                questoesFinal.Add(multiplaEscolha[item]);
            }

            //----------------------------------------------//
            for (int i = 0; i < provaFixa.QtVerdadeirFalso; i++)
            {
                QtVerdadeirFalso.Add(rnd.Next(0, verdadeirFalso.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtVerdadeirFalso)
            {
                questoesFinal.Add(verdadeirFalso[item]);
            }

            //----------------------------------------------//
            for (int i = 0; i < provaFixa.QtDissertativa; i++)
            {
                QtDissertativa.Add(rnd.Next(0, dissertativa.Count));

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