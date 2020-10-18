﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class RespostasAlunoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarResposta(RespostasAluno resp)
        {
            ctx.RespostasAlunos.Add(resp);
            ctx.SaveChanges();
            return true;
        }

        public static bool Editar(RespostasAluno resp)
        {
            ctx.Entry(resp).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        //public static List<RespostasAluno> ListarPorAlunoProva (int idProva, int idAluno)
        //{

        //    return ctx.RespostasAlunos.Include("Questao").Include("Alternativas").Include("Aluno").Where(x => x.RespostasAlunoId == idAluno && x.).ToList();
        //}

        public static List<RespostasAluno> PerguntasParaCorrigir(int id, int situac)
        {

            return ctx.RespostasAlunos.Include("Questao").Include("Alternativas").Include("Aluno").Where(x => x.Aluno.AlunoId == id && x.SituacaoCorrecao == situac).ToList();
        }

        public static List<RespostasAluno> PerguntasParaCorrigir( int situac)
        {

            return ctx.RespostasAlunos.Include("Questoes").Include("Alternativas").Include("Alunos").Where(x =>  x.RespostaDiscursiva != null).ToList();
        }

        public static RespostasAluno BuscarProvaQuestaoAluno(int QuestaoID, int ProvaID, int AlunoID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);
            RespostasAluno result = new RespostasAluno();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == AlunoID && item.Questao.QuestaoId == QuestaoID)
                    result = item;
            }

            return result;
        }

        public static bool VerificarSeProvaFeita(int ProvaID, int AlunoID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);
            bool result = false;
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == AlunoID && item.DataHoraInicio != null)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}