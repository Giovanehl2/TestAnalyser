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
            try
            {
                ctx.Provas.Add(prova);
                ctx.SaveChanges();
                ctx.Entry(prova).State = System.Data.Entity.EntityState.Detached;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }

        }
        public static bool EditarProva(Prova prova)
        {
            ctx.Entry(prova).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
        public static Prova BuscarProvaId(int id)
        {
            Prova prova = ctx.Provas.Include("ConfigPln")
                .Include("Professor")
                .Include("Professor.Disciplinas")
                .Include("Professor.Provas")
                .Include("NotasQuestoes")
                .Include("NotasQuestoes.Questao")
                .Include("Disciplina")
                .Include("Disciplina.Turmas")
                .Include("Disciplina.Professores")
                .Include("Disciplina.Cursos")
                .Include("Disciplina.Provas")
                .Include("Disciplina.Alunos")
                .Include("NotasQuestoes.Questao")
                .Include("NotasQuestoes.Questao.AssuntoQuestao")
                .Include("NotasQuestoes.Questao.Alternativas")
                .Include("RespostasAlunos")
                .Include("RespostasAlunos.Aluno")
                .Include("RespostasAlunos.Alternativas")
                .Include("RespostasAlunos.Questao")
                .FirstOrDefault(x => x.ProvaId == id);

            return prova;
        }
        public static List<Prova> BuscarPorProfessor(int idProfessor)
        {
            return ctx.Provas.Include("ConfigPln")
                .Include("Professor")
                .Include("Professor.Disciplinas")
                .Include("Professor.Provas")
                .Include("NotasQuestoes")
                .Include("NotasQuestoes.Questao")
                .Include("Disciplina")
                .Include("Disciplina.Turmas")
                .Include("Disciplina.Professores")
                .Include("Disciplina.Cursos")
                .Include("Disciplina.Provas")
                .Include("Disciplina.Alunos")
                .Include("NotasQuestoes.Questao")
                .Include("NotasQuestoes.Questao.AssuntoQuestao")
                .Include("NotasQuestoes.Questao.Alternativas")
                .Include("RespostasAlunos")
                .Include("RespostasAlunos.Aluno")
                .Include("RespostasAlunos.Alternativas")
                .Include("RespostasAlunos.Questao")
                .Where(p => p.Professor.ProfessorId == idProfessor).ToList();
        }

        public static Prova BuscarProvaDuplicada(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }
        public static Prova BuscarPorTituloProva(string titulo)
        {
            return ctx.Provas.Where(p => p.TituloProva.Equals(titulo)).FirstOrDefault();

        }

        public static List<Prova> BuscarProvasPesquisa(int idProfessor, DateTime DataInicio, DateTime DataFim, int Curso, int Disciplina, string Turma)
        {
            return ctx.Provas.Include("Professor").Include("Disciplina").Where(p => p.DataProvaInicio >= DataInicio && p.DataProvaFim <= DataFim && p.Professor.ProfessorId == idProfessor).ToList();
        }

        public static List<Prova> BuscarProvasPesquisa(int idProfessor, DateTime DataInicio, DateTime DataFim)
        {
            return ctx.Provas.Include("Professor").Include("Disciplina").Where(p => p.DataProvaInicio >= DataInicio && p.DataProvaFim <= DataFim && p.Professor.ProfessorId == idProfessor).ToList();
        }
        public static List<Prova> BuscarProvasPesquisa(int idProfessor, DateTime DataInicio, DateTime DataFim, int Curso, int Disciplina)
        {
            return ctx.Provas.Include("Professor").Include("Disciplina").Where(p => p.DataProvaInicio >= DataInicio && p.DataProvaFim <= DataFim && p.Professor.ProfessorId == idProfessor && p.Disciplina.DisciplinaId == Disciplina).ToList();
        }

        public static List<Prova> BuscarProvasPesquisa(int idProfessor, DateTime DataInicio, DateTime DataFim, int Curso)
        {
            List<Prova> provas = new List<Prova>();

            provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(p => p.DataProvaInicio >= DataInicio && p.DataProvaFim <= DataFim && p.Professor.ProfessorId == idProfessor).ToList(); 

            return provas;
        }

        public static List<Prova> BuscarProvasAluno(int Status, DateTime? DataIni, DateTime? DataFim, int Disciplina, int Turma)
        {
            List<Prova> result = new List<Prova>();
            List<Prova> provas = new List<Prova>();

            switch (Status)
            {
                case 1:
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.HoraInicio > DateTime.Now).ToList();
                    break;

                case 2:
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.HoraInicio <= DateTime.Now && x.HoraFim >= DateTime.Now).ToList();
                    break;

                case 3:
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.DataProvaFim < DateTime.Now).ToList();
                    break;

                default:
                    if (DataIni == null || DataFim == null)
                    {
                        DataIni = Convert.ToDateTime("01/01/2019");
                        DataFim = Convert.ToDateTime("01/01/2099");
                    }
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.DataProvaInicio >= DataIni && x.DataProvaFim <= DataFim).ToList();
                    break;
            }

            //var resultado = ctx.Provas.Where(x => x.DataProvaInicio >= DataIni && x.DataProvaFim <= DataFim)
            //    .Include(c => c.Disciplina)
            //    .Where(c => c.Disciplina.DisciplinaId == Disciplina).Include(p => p.Professor).Where(p => p.Professor.Matricula == matricula).ToList();
            if (Turma != 0)
            {
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
            }
            else
            {
                foreach (var item3 in provas)
                {
                    if (item3.Disciplina.DisciplinaId == Disciplina)
                    {
                        result.Add(item3);
                    }
                }
            }

            return result;
        }

        public static List<Prova> BuscarProvasAlunoGeral(int Status, DateTime? DataIni, DateTime? DataFim)
        {
            List<Prova> provas = new List<Prova>();

            switch (Status)
            {
                case 1:
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.HoraInicio > DateTime.Now).ToList();
                    break;

                case 2:
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.HoraInicio <= DateTime.Now && x.HoraFim >= DateTime.Now).ToList();
                    break;

                case 3:
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.DataProvaFim < DateTime.Now).ToList();
                    break;

                default:
                    if (DataIni == null || DataFim == null)
                    {
                        DataIni = Convert.ToDateTime("01/01/2019");
                        DataFim = Convert.ToDateTime("01/01/2099");
                    }
                    provas = ctx.Provas.Include("Professor").Include("Disciplina").Where(x => x.DataProvaInicio >= DataIni && x.DataProvaFim <= DataFim).ToList();
                    break;
            }

            return provas;
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
            Prova prova = ProvaDAO.BuscarProvaId(idProva);
            List<RespostasAluno> result = new List<RespostasAluno>();
            foreach (RespostasAluno item in prova.RespostasAlunos)
            {
                if (item.Aluno.AlunoId == idAluno)
                {
                    result.Add(item);
                }
            }

            //prova.RespostasAlunos.Clear();
            //prova.RespostasAlunos.AddRange(result);

            return result.OrderBy(x => x.RespostasAlunoId).ToList();
        }

        public static double BuscarValorNotamax(int ProvaID, int QuestaoID)
        {
            double notamax = 0;
            Prova prov = ctx.Provas.Include("NotasQuestoes").Include("NotasQuestoes.Questao").Include("NotasQuestoes.Questao.AssuntoQuestao").FirstOrDefault(x => x.ProvaId == ProvaID);
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
            return result.OrderBy(x=> x.Aluno.NomeAluno).ToList();
        }

        public static bool MostrarNota(int ProvaID)
        {
            bool mostrar = false;
            Prova prova = BuscarProvaId(ProvaID);
            if (DateTime.Now > prova.HoraFim)
            {
                mostrar = true;
            }
            return mostrar;
        }

    }
}