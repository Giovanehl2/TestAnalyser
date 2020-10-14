using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.DAL
{
    public class ProvaDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        /*o objeto deve vir validado e com todos os campos obrigatórios preenchidos*/
        public static bool CadastrarProva(Prova prova)
        {
            /*não estou incluindo regras e nem validações no momento*/
            if (BuscarProvaDuplicada(prova.TituloProva) == null)
            {
                ctx.Provas.Add(prova);
                ctx.SaveChanges();
                ctx.Entry(prova).State = System.Data.Entity.EntityState.Detached;
                return true;
            }
            return false;
        }
        public static bool EditarProva(Prova prova)
        {
            ctx.Entry(prova).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
        public static Prova BuscarProvaId(int id)
        {
            return ctx.Provas.Include("Professor").Include("NotasQuestoes").Include("RespostasAlunos")
                .Include("ConfigPln").Include("Disciplina").Include("NotasQuestoes.Questao")
                .Include("NotasQuestoes.Questao.Alternativas").Include("NotasQuestoes.Questao.Opcoes")
                .Include("RespostasAlunos.Aluno")
                .FirstOrDefault(x => x.ProvaId == id);
        }
        public static Prova BuscarProvaDuplicada(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }
        public static Prova BuscarPorTituloProva(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }

        public static List<Prova> BuscarProvasPesquisa(int idProfessor, DateTime DataInicio, DateTime DataFim, int Curso, int Disciplina, int Turma)
        {
            List<Prova> result = new List<Prova>();

            List<Prova>  provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(p => p.DataProva >= DataInicio && p.DataProva <= DataFim && p.Professor.ProfessorId == idProfessor).ToList();
            if(Disciplina == 0 || Turma == 0)
            {
                return provas;
            }
            else
            {
                foreach (var itemProva in provas)
                {
                    if (itemProva.Disciplina.DisciplinaId == Disciplina)
                    {
                        foreach (var item2 in itemProva.Disciplina.Turmas)
                        {

                            if (item2.TurmaId == Turma)
                            {
                                result.Add(itemProva);
                            }
                        }
                    }

                }

                return result;
            }
            

        }

        public static List<Prova> BuscarProvasAluno(int matricula, int Pendente, DateTime DataIni, DateTime DataFim, int Curso, int Disciplina, int Turma)
        {
            List<Prova> result = new List<Prova>();

            var resultado = ctx.Provas.Where(x => x.DataProva >= DataIni  && x.DataProva <= DataFim && x.StatusProva == Pendente)
                .Include(c => c.Disciplina)
                .Where(c => c.Disciplina.DisciplinaId == Disciplina).Include(p => p.Professor).Where(p => p.Professor.Matricula == matricula).ToList();


            List<Prova> provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.DataProva >= DataIni && x.DataProva <= DataFim && x.StatusProva == Pendente).ToList();

            foreach (var item in provas)
            {
                foreach (var item2 in item.Disciplina.Turmas)
                {
                    if (item2.TurmaId == Turma)
                    {
                        result.Add(item);
                    }
                }
            }

            return result;

        }
        public static List<Prova> BuscarProvasPorProfessor(int matricula)
        {
            return ctx.Provas.Where(p => p.Professor.Matricula == matricula).ToList();

        }

        public static List<Prova> BuscarPorStatus(int status)
        {
            return ctx.Provas.Where(p => p.StatusProva.Equals(status)).ToList();
        }
        public static List<RespostasAluno> BuscarRespostasPorAluno(int idAluno, int idProva)
        {

            Prova prova = ctx.Provas.Include("RespostasAlunos").Where(p => p.ProvaId == idProva).FirstOrDefault();
            List<RespostasAluno> result = new List<RespostasAluno>();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == idAluno)
                    result.Add(item);
            }
            prova.RespostasAlunos.Clear();
            prova.RespostasAlunos.AddRange(result);

            return prova.RespostasAlunos;
        }

        public static double BuscarValorNotamax(int ProvaID, int QuestaoID)
        {
            double notamax = 0;
            Prova prov = ctx.Provas.Include("NotasQuestoes").Include("NotasQuestoes.Questao").FirstOrDefault(x => x.ProvaId == ProvaID);
            foreach (var item in prov.NotasQuestoes)
            {
                if (item.Questao.QuestaoId == QuestaoID)
                {
                    notamax = item.ValorQuestao;
                    break;
                }
            }

            return notamax;
        }

        public static List<RespostasAluno> ListaAlunoPorProva(int idProva)
        {
            Prova prova = new Prova();
            List<RespostasAluno> result = new List<RespostasAluno>();
            prova = ctx.Provas.Include("RespostasAlunos").Where(x => x.ProvaId == idProva).FirstOrDefault();

            result = prova.RespostasAlunos.ToList().GroupBy(elem => elem.Aluno.AlunoId).Select(g => g.First()).ToList();
            return result.OrderBy(x=> x.Aluno.Nome).ToList();
        }

    }
}