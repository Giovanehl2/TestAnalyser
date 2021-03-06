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
            //ctx.Entry(resp).Property("Prova_ProvaId").IsModified = false;
            ctx.Entry(resp).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }

        public static List<RespostasAluno> BuscarRespostasAluno(int ProvaID, int AlunoID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);
            List<RespostasAluno> resps = new List<RespostasAluno>();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == AlunoID)
                    resps.Add(item);
            }

            return resps;
        }

        public static List<RespostasAluno> BuscarRespostasPorProvaId(int ProvaID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);

            return prova.RespostasAlunos;
        }

        //public static List<RespostasAluno> ListarPorAlunoProva (int idProva, int idAluno)
        //{

        //    return ctx.RespostasAlunos.Include("Questao").Include("Alternativas").Include("Aluno").Where(x => x.RespostasAlunoId == idAluno && x.).ToList();
        //}

        public static List<RespostasAluno> PerguntasParaCorrigir(int id, int situac)
        {

            return ctx.RespostasAlunos
                .Include("Questao")
                .Include("Questao.AssuntoQuestao")
                .Include("Alternativas")
                .Include("Alternativas.Questao")
                .Include("Alternativas.RespostaAluno")
                .Include("Aluno")
                .Include("Prova")
                .Where(x => x.Aluno.AlunoId == id && x.SituacaoCorrecao == situac).ToList();
        }

        public static List<RespostasAluno> PerguntasParaCorrigir(int situac)
        {

            return ctx.RespostasAlunos
                .Include("Questao")
                .Include("Questao.AssuntoQuestao")
                .Include("Alternativas")
                .Include("Alternativas.Questao")
                .Include("Alternativas.RespostaAluno")
                .Include("Aluno")
                .Include("Prova")
                .Where(x => x.RespostaDiscursiva != null).ToList();
        }
        public static RespostasAluno RespostasAlunoId(int id)
        {

            return ctx.RespostasAlunos
                .Include("Questao")
                .Include("Questao.AssuntoQuestao")
                .Include("Alternativas")
                .Include("Alternativas.Questao")
                .Include("Alternativas.RespostaAluno")
                .Include("Aluno")
                .Include("Prova")
                .Where(x => x.RespostasAlunoId == id).FirstOrDefault();
        }

        public static List<RespostasAluno> RespostasAlunoProvaId(int id)
        {

            return ctx.RespostasAlunos
                .Include("Questao")
                .Include("Questao.AssuntoQuestao")
                .Include("Alternativas")
                .Include("Alternativas.Questao")
                .Include("Alternativas.RespostaAluno")
                .Include("Aluno")
                .Include("Prova.ConfigPln")
                .Include("Prova.RespostasAlunos")
                .Include("Prova.Professor")
                .Include("Prova.NotasQuestoes")
                .Include("Prova.Disciplina")
                .Where(x => x.Prova.ProvaId == id).ToList();
        }

        public static RespostasAluno BuscarProvaQuestaoAluno(int QuestaoID, int ProvaID, int AlunoID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);
            RespostasAluno result = new RespostasAluno();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == AlunoID && item.Questao.QuestaoId == QuestaoID)
                    result = RespostasAlunoId(item.RespostasAlunoId);
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

        public static List<RespostasAluno> SolicitarRevisaoProva(int ProvaID, int AlunoID)
        {
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);
            List<RespostasAluno> result = new List<RespostasAluno>();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == AlunoID && item.RespostaDiscursiva != null)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static List<int> BuscarAltsMarcadas(int ProvaID, int AlunoID)
        {
            List<int> lista = new List<int>();
            List<RespostasAluno> respostaAluno = new List<RespostasAluno>();
            Prova prova = ProvaDAO.BuscarProvaId(ProvaID);
            List<RespostasAluno> Respostas = new List<RespostasAluno>();
            Respostas = prova.RespostasAlunos.OrderBy(x => x.RespostasAlunoId).ToList();

            foreach (var item in Respostas)
            {
                if (item.Aluno.AlunoId == AlunoID)
                    respostaAluno.Add(item);
            }

            foreach (RespostasAluno item2 in respostaAluno)
            {
                foreach (var item3 in item2.Alternativas)
                {
                    lista.Add(item3.AlternativaId);
                }
            }

            return lista;
        }
    }
}