using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class CadastrarQuestoesController : Controller
    {
        // GET: CadastrarQuestoes
        //ActionResult para apresetar a tela de cadastro de questões...
        public ActionResult CadastrarQuestoes()
        {
            ViewBag.Disciplinas = DisciplinaDAO.ListarDisciplinas();
            //ViewBag.Person = new Person();
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarQuestoes(Questao questao)
        {

            return View(questao);
        }

        //Metodo para cadastrar a questão de tipo especifico no banco...
        [HttpPost]
        public ActionResult CadastrarSE(Questao questao, string DisciplinaId, List<string>Alternativas)
        {
            //Salva o tipo de questão, deixa-o ativo e mantem a RespostaDiscursiva vazia...
            questao.TipoQuestao = 1;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";

            //Pega o id da disciplina na view e pega o objeto todo para enviar no cadastro...
            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            //Salva a questão e rotorna ela com o ID...
            var QuestaoID = QuestaoDAO.CadastrarQuestao(questao);

            //Pega todas as alterativas preenchidas e salva.
            foreach (var item in Alternativas)
            {
                if(item != "")
                {
                    Alternativa alt = new Alternativa();
                    alt.DescAlternativa = item;
                    alt.correto = 0;
                    alt.Questao = QuestaoID;
                    //AlternativaDAO.CadastrarAlternativa(alt);
                }
            }

            //Podemos dar uma mensagem antes de retornar a view.
            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        [HttpPost]
        public ActionResult CadastrarME(Questao questao, string DisciplinaId, List<string> Alternativas)
        {
            questao.TipoQuestao = 2;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";

            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            var QuestaoID = QuestaoDAO.CadastrarQuestao(questao);

            foreach (var item in Alternativas)
            {
                if (item != "")
                {
                    Alternativa alt = new Alternativa();
                    alt.DescAlternativa = item;
                    alt.correto = 0;
                    alt.Questao = QuestaoID;
                   // AlternativaDAO.CadastrarAlternativa(alt);
                }
            }

            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        [HttpPost]
        public ActionResult CadastrarVF(Questao questao, string DisciplinaId, List<string> Alternativas)
        {
            questao.TipoQuestao = 3;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";

            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            var QuestaoID = QuestaoDAO.CadastrarQuestao(questao);

            foreach (var item in Alternativas)
            {
                if (item != "")
                {
                    Alternativa alt = new Alternativa();
                    alt.DescAlternativa = item;
                    alt.correto = 0;
                    alt.Questao = QuestaoID;
                    //AlternativaDAO.CadastrarAlternativa(alt);
                }
            }

            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        [HttpPost]
        public ActionResult CadastrarDT(Questao questao, string DisciplinaId)
        {
            questao.TipoQuestao = 4;
            questao.situacao = 1;

            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            QuestaoDAO.CadastrarQuestao(questao);

            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }
    }
}


//pendencias do gabriel:
//Salvar as opções...
//pegar a alternativa correta...
//remover o campo textarea html da resposta dissertativa...