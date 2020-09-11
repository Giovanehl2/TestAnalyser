﻿using Microsoft.Ajax.Utilities;
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

        public static void ExcDesQuestao(int id)
        {
            Questao q = BuscarQuestaoId(id);
            if(q.situacao == 1)
            {
                q.situacao = 0;
                AlterarQuestao(q);
            }
            else
            {
                ctx.Entry(q).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

        }

        public static void AlterarQuestao(Questao questao)
        {
            ctx.Entry(questao).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public static List<String> BuscarAssuntos(string disc)
        {
            List<String> assuntos = new List<String>();
            List<Questao> questao = new List<Questao>();

            questao = ctx.Questoes.Include("Disciplina").Where(x => x.Disciplina.Nome == disc && x.situacao == 1).ToList();
            foreach (Questao item in questao)
            {
                assuntos.Add(item.Assunto);

            }

            return assuntos;

        }

        public static Questao BuscarQuestaoId(int id)
        {
            return ctx.Questoes.Find(id);
        }

        public static List<Questao> BuscarPorAssunto(string assunto)
        {
            return ctx.Questoes.Include("Opcoes").Include("Alternativas").Where(p => p.Assunto.Equals(assunto) && p.situacao == 1).ToList();

        }

        public static List<Questao> BuscarPorDisciplina(string disciplina, int? Des)
        {
            if(Des != null)
            {
                Disciplina disc = DisciplinaDAO.BuscarPorNome(disciplina);
                return ctx.Questoes.Include("Opcoes").Include("Alternativas").Where(
                    p => p.Disciplina.DisciplinaId.Equals(disc.DisciplinaId) && p.situacao == 0).ToList();
            }
            else
            {
                Disciplina disc = DisciplinaDAO.BuscarPorNome(disciplina);
                return ctx.Questoes.Include("Opcoes").Include("Alternativas").Where(
                    p => p.Disciplina.DisciplinaId.Equals(disc.DisciplinaId) && p.situacao == 1).ToList();
            }
            
        }

        public static List<Questao> BuscarPorTpQuestao(int tpPerg)
        {
            return ctx.Questoes.Where(p => p.TipoQuestao.Equals(tpPerg)).ToList();
        }
    }
}