using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using TestAnalyser.Model;
using Context = TestAnalyser.Model.Context;

namespace TestAnalyser.DAL
{
    public class QuestaoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static Questao CadastrarQuestao(Questao questao)
        {
            //var id = "0";
            var obj = new Questao();
            /*não estou incluindo regras e nem validações no momento*/
            if (questao.Opcoes != null)
            { 
                ctx.Questoes.Add(questao);
                ctx.SaveChanges();
                obj = questao;
                //id = Convert.ToString(questao.QuestaoId);
                return obj;
            }
            return obj;
        }
        public static void RemoverQuestao(Questao questao)
        {
            /*verifica se a questão não esta atrelada a alguma prova, caso contrario apenas desabilita a utilização desta questão*/
            if (ProvaDAO.VerificarQuestaoCadastrada(questao.QuestaoId) == null)
            {
                ctx.Questoes.Remove(questao);
                ctx.SaveChanges();
            }
            else
            {
                questao.situacao = 0;
                AlterarQuestao(questao);
            }

        }

        public static void AlterarQuestao(Questao questao)
        {
            ctx.Entry(questao).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public static List<String> BuscarAssuntoPorDisciplina(int disciplina)
        {
            return ctx.Questoes.Include(d => d.Disciplina).Select(z => z.Assunto).Distinct().ToList();

        }
        public static Questao BuscarQuestaoId(int id)
        {
            return ctx.Questoes.Find(id);
        }

        public static List<Questao> BuscarPorAssunto(string assunto)
        {
            return ctx.Questoes.Where(p => p.Assunto.Equals(assunto)).ToList(); 
        }
        public static List<Questao> BuscarPorTpQuestao(int tpPerg)
        {
            return ctx.Questoes.Where(p => p.TipoQuestao.Equals(tpPerg)).ToList();
        }
    }
}