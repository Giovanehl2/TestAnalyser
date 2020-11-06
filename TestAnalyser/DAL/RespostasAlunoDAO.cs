using System;
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

        //public static List<RespostasAluno> ListarPorAlunoProva (int idProva, int idAluno)
        //{

        //    return ctx.RespostasAlunos.Include("Questao").Include("Alternativas").Include("Aluno").Where(x => x.RespostasAlunoId == idAluno && x.).ToList();
        //}

        public static List<RespostasAluno> PerguntasParaCorrigir(int id, int situac)
        {

            return ctx.RespostasAlunos.Include("Questao").Include("Alternativas").Include("Aluno").Where(x => x.Aluno.AlunoId == id && x.SituacaoCorrecao == situac).ToList();
        }

        public static List<RespostasAluno> PerguntasParaCorrigir(int situac)
        {

            return ctx.RespostasAlunos.Include("Questoes").Include("Alternativas").Include("Alunos").Where(x => x.RespostaDiscursiva != null).ToList();
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

            foreach (var item in prova.RespostasAlunos)
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