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
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            Prova prova = new Prova();

            List<string> assuntos = new List<string>();


            List<Curso> cursos = new List<Curso>();
            Curso curso;
            int id = Convert.ToInt32(Session["IdUsr"]);
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

        public ActionResult SalvarProva(Prova prova)
        {
            List<Aluno> alunos = new List<Aluno>();
            Disciplina disc = DisciplinaDAO.BuscarPorNome(prova.NomeDisciplina);
            Turma turma = TurmaDAO.BuscarTurmaNome(prova.NomeTurma);

            //prova.Alunos = turma.Alunos;
            prova.Disciplina = disc;

            int id = Convert.ToInt32(Session["IdUsr"]);
            Professor professor= ProfessorDAO.BuscarProfessorPorId(id);

            provaFixa = prova;



            return RedirectToAction("AdicionarQuestoesNaProva", "GerarProva");
        }

        public ActionResult AdicionarQuestoesNaProva()
        {

            return View(provaFixa);
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
            //gera as sequencias aleatorias de acordo com cada tipo de questão
            for (int i = 0; i < provaFixa.QtSimplesEscolha; i++)
            {
                QtSimplesEscolha.Add(rnd.Next(simplesEscolha.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtSimplesEscolha)
            {
                questoesFinal.Add(simplesEscolha[item]);
            }


            for (int i = 0; i < provaFixa.QtMultiplaEscolha; i++)
            {
                QtSimplesEscolha.Add(rnd.Next(multiplaEscolha.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtSimplesEscolha)
            {
                questoesFinal.Add(multiplaEscolha[item]);
            }


            for (int i = 0; i < provaFixa.QtVerdadeirFalso; i++)
            {
                QtSimplesEscolha.Add(rnd.Next(verdadeirFalso.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtSimplesEscolha)
            {
                questoesFinal.Add(verdadeirFalso[item]);
            }


            for (int i = 0; i < provaFixa.QtDissertativa; i++)
            {
                QtSimplesEscolha.Add(rnd.Next(dissertativa.Count));

            }
            //adiciona as questões na lista final
            foreach (int item in QtSimplesEscolha)
            {
                questoesFinal.Add(dissertativa[item]);
            }

            return questoesFinal;

        }
    }
}