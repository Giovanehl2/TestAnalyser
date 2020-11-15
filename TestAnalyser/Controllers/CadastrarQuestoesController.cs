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
    public class CadastrarQuestoesController : Controller
    {
        //GET: CadastrarQuestoes
        //ActionResult para apresetar a tela de cadastro de questões...
        public ActionResult CadastrarQuestoes()
        {
            ViewBag.AssuntosQuestao = QuestaoDAO.BuscarAssuntosQuestaoTodos();
            ViewBag.Disciplinas = DisciplinaDAO.ListarDisciplinas();
            return View();
        }

        public ActionResult CadastrarAssunto()
        {
            ViewBag.Assuntos = QuestaoDAO.BuscarAssuntosQuestaoTodos();
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarQuestoes(Questao questao)
        {

            return View(questao);
        }

        //Metodo para cadastrar a questão de tipo especifico no banco...
        [HttpPost]
        public ActionResult CadastrarSE(Questao questao, string AssuntoQuestao, string DisciplinaId, List<string>Alternativas, int? SEradio)
        {
            if (!SEradio.HasValue)
            {
                TempData["$AlertMessage$"] = "Favor selecionar uma alternativa correta!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }
            if (questao.Enunciado == null || questao.Enunciado.Equals(""))
            {
                TempData["$AlertMessage$"] = "Favor prencher o enunciado da questão!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }
            //Salva o tipo de questão, deixa-o ativo e mantem a RespostaDiscursiva vazia...
            questao.TipoQuestao = 1;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";

            //Pega o id da disciplina na view e pega o objeto todo para enviar no cadastro...
            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            var y = QuestaoDAO.BuscarAssuntoId(Convert.ToInt32(AssuntoQuestao));
            questao.AssuntoQuestao = y;

            //Salva a questão e rotorna ela com o ID...
            var QuestaoID = QuestaoDAO.CadastrarQuestao(questao);

            //Pega todas as alterativas preenchidas e salva.
            int count = 1;
            foreach (var item in Alternativas)
            {
                if(item != "")
                {
                    Alternativa alt = new Alternativa();
                    alt.DescAlternativa = item;
                    if (count == SEradio) { 
                        alt.correto = 1; 
                    } else { 
                        alt.correto = 0; 
                    }
                    alt.Questao = QuestaoID;
                    AlternativaDAO.CadastrarAlternativa(alt);
                }
                count++;
            }
            TempData["$AlertMessage$"] = "Questão Criada com Sucesso";
            //Podemos dar uma mensagem antes de retornar a view.
            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        [HttpPost]
        public ActionResult CadastrarME(Questao questao, string AssuntoQuestao, string DisciplinaId, List<string> Alternativas, List<string> Opcoes, List<int?>MEchbx)
        {

            if (MEchbx == null || questao.Enunciado.Equals(""))
            {
                TempData["$AlertMessage$"] = "Favor selecionar uma alternativa correta!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }
            if (questao.Enunciado == null || questao.Enunciado.Equals(""))
            {
                TempData["$AlertMessage$"] = "Favor prencher o enunciado da questão!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }
            questao.TipoQuestao = 2;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";

            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            var y = QuestaoDAO.BuscarAssuntoId(Convert.ToInt32(AssuntoQuestao));
            questao.AssuntoQuestao = y;

            var QuestaoID = QuestaoDAO.CadastrarQuestao(questao);

            //foreach (var item in Opcoes)
            //{
            //    if (item != "")
            //    {
            //        Opcao opt = new Opcao();
            //        opt.descricao = item;
            //        opt.Questao = QuestaoID;
            //        OpcaoDAO.CadastrarOpcao(opt);
            //    }
            //}

            int count = 1;
            foreach (var item in Alternativas)
            {
                if (item != "")
                {
                    Alternativa alt = new Alternativa();
                    alt.DescAlternativa = item;
                    if (MEchbx.Contains(count)) {
                        alt.correto = 1;
                    } else {
                        alt.correto = 0;
                    }
                    alt.Questao = QuestaoID;
                    AlternativaDAO.CadastrarAlternativa(alt);
                }
                count++;
            }
            TempData["$AlertMessage$"] = "Questão Criada com Sucesso";
            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        [HttpPost]
        public ActionResult CadastrarVF(Questao questao, string AssuntoQuestao, string DisciplinaId, List<string> Alternativas, List<string> Opcoes, List<int?>VFchbx)
        
        {
            if (questao.Enunciado == null || questao.Enunciado.Equals(""))
            {
                TempData["$AlertMessage$"] = "Favor prencher o enunciado da questão!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }
            questao.TipoQuestao = 3;
            questao.situacao = 1;
            questao.RespostaDiscursiva = "";

            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            var y = QuestaoDAO.BuscarAssuntoId(Convert.ToInt32(AssuntoQuestao));
            questao.AssuntoQuestao = y;

            var QuestaoID = QuestaoDAO.CadastrarQuestao(questao);

            //foreach (var item in Opcoes)
            //{
            //    if (item != "")
            //    {
            //        Opcao opt = new Opcao();
            //        opt.descricao = item;
            //        opt.Questao = QuestaoID;
            //        OpcaoDAO.CadastrarOpcao(opt);
            //    }
            //}

            int count = 1;
            foreach (var item in Alternativas)
            {
                if (item != "")
                {
                    Alternativa alt = new Alternativa();
                    alt.DescAlternativa = item;
                    if (VFchbx.Contains(count))
                    {
                        alt.correto = 1;
                    }
                    else
                    {
                        alt.correto = 0;
                    }
                    alt.Questao = QuestaoID;
                    AlternativaDAO.CadastrarAlternativa(alt);
                }
                count++;
            }
            TempData["$AlertMessage$"] = "Questão Criada com Sucesso";
            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        [HttpPost]
        public ActionResult CadastrarDT(Questao questao, string AssuntoQuestao, string DisciplinaId)
        {
            if (questao.Enunciado == null || questao.Enunciado.Equals("") )
            {
                TempData["$AlertMessage$"] = "Favor prencher o enunciado da questão!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }

            if (questao.RespostaDiscursiva == null || questao.RespostaDiscursiva.Equals(""))
            {
                TempData["$AlertMessage$"] = "Favor prencher a resposta da questão!";
                return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
            }

            questao.TipoQuestao = 4;
            questao.situacao = 1;

            var x = DisciplinaDAO.BuscarDisciplinaId(Convert.ToInt32(DisciplinaId));
            questao.Disciplina = x;

            var y = QuestaoDAO.BuscarAssuntoId(Convert.ToInt32(AssuntoQuestao));
            questao.AssuntoQuestao = y;

            QuestaoDAO.CadastrarQuestao(questao);
            TempData["$AlertMessage$"] = "Questão Criada com Sucesso";
            return RedirectToAction("CadastrarQuestoes", "CadastrarQuestoes");
        }

        public ActionResult CadastrarAssuntos(string AssuntoQ)
        {
            Assunto assunto = new Assunto();
            assunto.Descricao = AssuntoQ;

            QuestaoDAO.CadastrarAssunto(assunto);
            TempData["$CadAssunto$"] = "Assunto Criado com Sucesso";
            return RedirectToAction("CadastrarAssunto", "CadastrarQuestoes");
        }
        public ActionResult EditarAssuntos(int AssuntoID, string AssuntoDesc)
        {
            Assunto assunto = QuestaoDAO.BuscarAssuntoId(AssuntoID);
            assunto.Descricao = "teste";

            QuestaoDAO.EditarAssunto(assunto);
            TempData["$CadAssunto$"] = "Assunto Alterado com Sucesso";
            return RedirectToAction("CadastrarAssunto", "CadastrarQuestoes");
        }
        public ActionResult RemoverAssuntos(int AssuntoID)
        {
            Assunto assunto = QuestaoDAO.BuscarAssuntoId(AssuntoID);
            QuestaoDAO.RemoverAssunto(assunto);

            TempData["$CadAssunto$"] = "Assunto Removido com Sucesso";
            return RedirectToAction("CadastrarAssunto", "CadastrarQuestoes");
        }
    }
}