using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class Context : DbContext
    {
        public Context() : base("DBTestAnalyser") { }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Prova> Provas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Opcao> Opcoes { get; set; }
        public DbSet<RespostasAluno> RespostasAlunos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NotaQuestao> NotasQuestoes { get; set; }
        public DbSet<AlunoNota> AlunoNotas { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ConfigPln> ConfigsPln { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }

        
    }

    /*   
        Install-Package EntityFramework
        Enable-Migrations
        Add-Migration teste
        Update-Database -verbose
        Unistall-Package EntityFramework 
     */

}