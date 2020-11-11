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
                return obj;
            }
            return obj;
        }

        public static bool ExcDesQuestao(int id)
        {
            try
            {
                Questao q = BuscarQuestaoId(id);
                if (q.situacao == 1)
                {
                    q.situacao = 0;
                    AlterarQuestao(q);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return false;
        }

        public static void AlterarQuestao(Questao questao)
        {
            ctx.Entry(questao).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public static void SalvarQuestao(Questao questao)
        {
            Questao Q = ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").First(i => i.QuestaoId == questao.QuestaoId);
            ctx.Entry(Q).CurrentValues.SetValues(questao);
            for (int i = 0; i < questao.Alternativas.Count; i++)
            {
                Q.Alternativas[i].DescAlternativa = questao.Alternativas[i].DescAlternativa;
                Q.Alternativas[i].correto = questao.Alternativas[i].correto;
            }
            //for (int h = 0; h < questao.Opcoes.Count; h++)
            //{
            //    Q.Opcoes[h].descricao = questao.Opcoes[h].descricao;
            //}

            ctx.SaveChanges();
        }

        public static List<Assunto> BuscarAssuntosQuestaoTodos()
        {
            return ctx.AssuntosQuestao.ToList();
        }

        public static Assunto BuscarAssuntoId(int id)
        {
            return ctx.AssuntosQuestao.Find(id);
        }

        public static void CadastrarAssunto(Assunto assunto)
        {
            ctx.AssuntosQuestao.Add(assunto);
            ctx.SaveChanges();
        }

        public static void EditarAssunto(Assunto assunto)
        {
            ctx.Entry(assunto).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public static void RemoverAssunto(Assunto assunto)
        {
            ctx.AssuntosQuestao.Remove(assunto);
            ctx.SaveChanges();
        }

        public static List<String> BuscarAssuntos(string disc)
        {
            List<String> assuntos = new List<String>();
            List<Questao> questao = new List<Questao>();

            questao = ctx.Questoes.Include("Disciplina").Include("AssuntoQuestao").Where(x => x.Disciplina.Nome == disc && x.situacao == 1).ToList();
            foreach (Questao item in questao)
            {
                if (item.AssuntoQuestao.Descricao != null)
                    { assuntos.Add(item.AssuntoQuestao.Descricao); }
                else
                    { assuntos.Add("Sem Assunto"); }
            }

            return assuntos;

        }

        public static Questao BuscarQuestaoId(int id)
        {
            return ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").Where(q => q.QuestaoId == id).FirstOrDefault();
        }

        public static List<Questao> BuscarTodasQuestoes(int? Des)
        {
            if (Des == null)
                { return ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").Where(p => p.situacao == 1).ToList(); }
            else 
                { return ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").Where(p => p.situacao == 0).ToList(); }
        }

        public static List<Questao> BuscarPorAssunto(string assunto)
        {
            return ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").Where(p => p.AssuntoQuestao.Descricao.Equals(assunto) && p.situacao == 1).ToList();
        }

        public static List<Questao> BuscarPorDisciplina(string disciplina, int? Des)
        {
            if(Des != null)
            {
                Disciplina disc = DisciplinaDAO.BuscarPorNome(disciplina);
                return ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").Where(
                    p => p.Disciplina.DisciplinaId.Equals(disc.DisciplinaId) && p.situacao == 0).ToList();
            }
            else
            {
                Disciplina disc = DisciplinaDAO.BuscarPorNome(disciplina);
                return ctx.Questoes.Include("Opcoes").Include("Alternativas").Include("AssuntoQuestao").Where(
                    p => p.Disciplina.DisciplinaId.Equals(disc.DisciplinaId) && p.situacao == 1).ToList();
            }
            
        }

        public static List<Questao> BuscarPorTpQuestao(int tpPerg)
        {
            return ctx.Questoes.Include("Alternativas").Include("AssuntoQuestao").Where(p => p.TipoQuestao.Equals(tpPerg)).ToList();
        }
    }
}