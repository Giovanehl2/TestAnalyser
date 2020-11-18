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
    public class ConfiguracoesController : Controller
    {
        // GET: Configuracoes
        public ActionResult Configuracoes()
        {
            var configs = AdminDAO.MostrarConfigsTela();
            return View(configs);
        }

        public ActionResult SalvarConfiguracoes(Configuracao configuracoes)
        {

            AdminDAO.SalvarConfiguracoes(configuracoes);

            return RedirectToAction("Configuracoes", "Configuracoes");
        }

        public string ImportarDadosApi()
        {
            
           if (ApiIntegracaoController.Importar(AdminDAO.BuscarConfiguracoes()))
            return "Carga realizada com sucesso";

            return "Erro inesperado durante a carga, favor entrar em contato com o suporte!";
        }

        public static void ValidarCorrecoesDiscursivas()
        {

            List<Prova> provas = ProvaDAO.BuscarTodasProvas();

            foreach (var item in provas)
            {
                List<RespostasAluno> respostas = RespostasAlunoDAO.BuscarRespostasPorProvaId(item.ProvaId);
                respostas = respostas.Where(x => x.RespostaDiscursiva != null).ToList();

                foreach (var item2 in respostas)
                {
                    if (item2.SituacaoCorrecao == 0 && item2.NotaAluno != 0)
                    {
                        var incorreto = item.ConfigPln.IncorretoFim;
                        var parcial = item.ConfigPln.RevisarFim;
                        var notaMAX = ProvaDAO.BuscarValorNotamax(item.ProvaId, item2.Questao.QuestaoId);

                        if (item2.NotaAluno <= incorreto)
                        {
                            item2.SituacaoCorrecao = 3;
                            item2.NotaAluno = 0;
                        }
                        else if (item2.NotaAluno <= parcial)
                        {
                            item2.SituacaoCorrecao = 2;
                            item2.NotaAluno = 0;
                        }
                        else
                        {
                            item2.SituacaoCorrecao = 1;
                            item2.NotaAluno = notaMAX;
                        }

                        RespostasAlunoDAO.Editar(item2);
                    }

                }
            
            }

        }

    }
}