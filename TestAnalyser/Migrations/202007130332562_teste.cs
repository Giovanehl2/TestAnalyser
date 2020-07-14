﻿namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId)
                .Index(t => t.Usuario_UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Matricula = c.Int(nullable: false),
                        Senha = c.String(),
                        TipoUsr = c.Int(nullable: false),
                        Alunos_AlunoId = c.Int(),
                        Configuracao_ConfiguracaoId = c.Int(),
                        Professor_ProfessorId = c.Int(),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Alunos", t => t.Alunos_AlunoId)
                .ForeignKey("dbo.Configuracoes", t => t.Configuracao_ConfiguracaoId)
                .ForeignKey("dbo.Professores", t => t.Professor_ProfessorId)
                .Index(t => t.Alunos_AlunoId)
                .Index(t => t.Configuracao_ConfiguracaoId)
                .Index(t => t.Professor_ProfessorId);
            
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        AlunoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AlunoId);
            
            CreateTable(
                "dbo.Provas",
                c => new
                    {
                        ProvaId = c.Int(nullable: false, identity: true),
                        TituloProva = c.String(),
                        ValorProva = c.Double(nullable: false),
                        NotaProva = c.Double(nullable: false),
                        StatusProva = c.Int(nullable: false),
                        DataProva = c.DateTime(nullable: false),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraFim = c.DateTime(nullable: false),
                        ConfigPln_ConfigPlnId = c.Int(),
                        Professor_ProfessorId = c.Int(),
                    })
                .PrimaryKey(t => t.ProvaId)
                .ForeignKey("dbo.ConfigsPln", t => t.ConfigPln_ConfigPlnId)
                .ForeignKey("dbo.Professores", t => t.Professor_ProfessorId)
                .Index(t => t.ConfigPln_ConfigPlnId)
                .Index(t => t.Professor_ProfessorId);
            
            CreateTable(
                "dbo.ConfigsPln",
                c => new
                    {
                        ConfigPlnId = c.Int(nullable: false, identity: true),
                        IncorretoInicio = c.Int(nullable: false),
                        IncorretoFim = c.Int(nullable: false),
                        RevisarInicio = c.Int(nullable: false),
                        RevisarFim = c.Int(nullable: false),
                        CorretoInicio = c.Int(nullable: false),
                        CorretoFim = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConfigPlnId);
            
            CreateTable(
                "dbo.Professores",
                c => new
                    {
                        ProfessorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ProfessorId);
            
            CreateTable(
                "dbo.Disciplinas",
                c => new
                    {
                        DisciplinaId = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        descrição = c.String(),
                    })
                .PrimaryKey(t => t.DisciplinaId);
            
            CreateTable(
                "dbo.Cursos",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity: true),
                        nomeCurso = c.String(),
                        descricao = c.String(),
                    })
                .PrimaryKey(t => t.CursoId);
            
            CreateTable(
                "dbo.Turmas",
                c => new
                    {
                        TurmaId = c.Int(nullable: false, identity: true),
                        NomeTurma = c.String(),
                        Periodo = c.String(),
                    })
                .PrimaryKey(t => t.TurmaId);
            
            CreateTable(
                "dbo.RespostasAlunos",
                c => new
                    {
                        RespostasAlunoId = c.Int(nullable: false, identity: true),
                        RespostaDiscursiva = c.String(),
                        Questao_QuestaoId = c.Int(),
                        Prova_ProvaId = c.Int(),
                    })
                .PrimaryKey(t => t.RespostasAlunoId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .ForeignKey("dbo.Provas", t => t.Prova_ProvaId)
                .Index(t => t.Questao_QuestaoId)
                .Index(t => t.Prova_ProvaId);
            
            CreateTable(
                "dbo.Alternativas",
                c => new
                    {
                        AlternativasId = c.Int(nullable: false, identity: true),
                        DescAlternativa = c.String(),
                        correto = c.Int(nullable: false),
                        Questao_QuestaoId = c.Int(),
                        RespostasAluno_RespostasAlunoId = c.Int(),
                    })
                .PrimaryKey(t => t.AlternativasId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .ForeignKey("dbo.RespostasAlunos", t => t.RespostasAluno_RespostasAlunoId)
                .Index(t => t.Questao_QuestaoId)
                .Index(t => t.RespostasAluno_RespostasAlunoId);
            
            CreateTable(
                "dbo.Questoes",
                c => new
                    {
                        QuestaoId = c.Int(nullable: false, identity: true),
                        assunto = c.String(),
                        Enunciado = c.String(),
                        TipoQuestao = c.Int(nullable: false),
                        RespostaDiscursiva = c.String(),
                        Disciplina_DisciplinaId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestaoId)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId)
                .Index(t => t.Disciplina_DisciplinaId);
            
            CreateTable(
                "dbo.Opcoes",
                c => new
                    {
                        OpcaoId = c.Int(nullable: false, identity: true),
                        descricao = c.String(),
                        Questao_QuestaoId = c.Int(),
                    })
                .PrimaryKey(t => t.OpcaoId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .Index(t => t.Questao_QuestaoId);
            
            CreateTable(
                "dbo.Configuracoes",
                c => new
                    {
                        ConfiguracaoId = c.Int(nullable: false, identity: true),
                        UrlApi = c.String(),
                        sincAuto = c.DateTime(nullable: false),
                        HoraCorrecao = c.DateTime(nullable: false),
                        Instituicao = c.Int(nullable: false),
                        Cnpj = c.String(),
                        ServidorEmail = c.String(),
                        CaixaSaida = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.ConfiguracaoId);
            
            CreateTable(
                "dbo.ProvaAlunoes",
                c => new
                    {
                        Prova_ProvaId = c.Int(nullable: false),
                        Aluno_AlunoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Prova_ProvaId, t.Aluno_AlunoId })
                .ForeignKey("dbo.Provas", t => t.Prova_ProvaId, cascadeDelete: true)
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId, cascadeDelete: true)
                .Index(t => t.Prova_ProvaId)
                .Index(t => t.Aluno_AlunoId);
            
            CreateTable(
                "dbo.CursoDisciplinas",
                c => new
                    {
                        Curso_CursoId = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Curso_CursoId, t.Disciplina_DisciplinaId })
                .ForeignKey("dbo.Cursos", t => t.Curso_CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId, cascadeDelete: true)
                .Index(t => t.Curso_CursoId)
                .Index(t => t.Disciplina_DisciplinaId);
            
            CreateTable(
                "dbo.DisciplinaProfessors",
                c => new
                    {
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                        Professor_ProfessorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Disciplina_DisciplinaId, t.Professor_ProfessorId })
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId, cascadeDelete: true)
                .ForeignKey("dbo.Professores", t => t.Professor_ProfessorId, cascadeDelete: true)
                .Index(t => t.Disciplina_DisciplinaId)
                .Index(t => t.Professor_ProfessorId);
            
            CreateTable(
                "dbo.TurmaAlunoes",
                c => new
                    {
                        Turma_TurmaId = c.Int(nullable: false),
                        Aluno_AlunoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Turma_TurmaId, t.Aluno_AlunoId })
                .ForeignKey("dbo.Turmas", t => t.Turma_TurmaId, cascadeDelete: true)
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId, cascadeDelete: true)
                .Index(t => t.Turma_TurmaId)
                .Index(t => t.Aluno_AlunoId);
            
            CreateTable(
                "dbo.TurmaDisciplinas",
                c => new
                    {
                        Turma_TurmaId = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Turma_TurmaId, t.Disciplina_DisciplinaId })
                .ForeignKey("dbo.Turmas", t => t.Turma_TurmaId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId, cascadeDelete: true)
                .Index(t => t.Turma_TurmaId)
                .Index(t => t.Disciplina_DisciplinaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "Usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "Professor_ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.Usuarios", "Configuracao_ConfiguracaoId", "dbo.Configuracoes");
            DropForeignKey("dbo.Usuarios", "Alunos_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.RespostasAlunos", "Prova_ProvaId", "dbo.Provas");
            DropForeignKey("dbo.RespostasAlunos", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.Alternativas", "RespostasAluno_RespostasAlunoId", "dbo.RespostasAlunos");
            DropForeignKey("dbo.Alternativas", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.Opcoes", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.Questoes", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.Provas", "Professor_ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.TurmaDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.TurmaDisciplinas", "Turma_TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.TurmaAlunoes", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.TurmaAlunoes", "Turma_TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.DisciplinaProfessors", "Professor_ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.DisciplinaProfessors", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.CursoDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.CursoDisciplinas", "Curso_CursoId", "dbo.Cursos");
            DropForeignKey("dbo.Provas", "ConfigPln_ConfigPlnId", "dbo.ConfigsPln");
            DropForeignKey("dbo.ProvaAlunoes", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.ProvaAlunoes", "Prova_ProvaId", "dbo.Provas");
            DropIndex("dbo.TurmaDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.TurmaDisciplinas", new[] { "Turma_TurmaId" });
            DropIndex("dbo.TurmaAlunoes", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.TurmaAlunoes", new[] { "Turma_TurmaId" });
            DropIndex("dbo.DisciplinaProfessors", new[] { "Professor_ProfessorId" });
            DropIndex("dbo.DisciplinaProfessors", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.CursoDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.CursoDisciplinas", new[] { "Curso_CursoId" });
            DropIndex("dbo.ProvaAlunoes", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.ProvaAlunoes", new[] { "Prova_ProvaId" });
            DropIndex("dbo.Opcoes", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.Questoes", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.Alternativas", new[] { "RespostasAluno_RespostasAlunoId" });
            DropIndex("dbo.Alternativas", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.RespostasAlunos", new[] { "Prova_ProvaId" });
            DropIndex("dbo.RespostasAlunos", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.Provas", new[] { "Professor_ProfessorId" });
            DropIndex("dbo.Provas", new[] { "ConfigPln_ConfigPlnId" });
            DropIndex("dbo.Usuarios", new[] { "Professor_ProfessorId" });
            DropIndex("dbo.Usuarios", new[] { "Configuracao_ConfiguracaoId" });
            DropIndex("dbo.Usuarios", new[] { "Alunos_AlunoId" });
            DropIndex("dbo.Admins", new[] { "Usuario_UsuarioId" });
            DropTable("dbo.TurmaDisciplinas");
            DropTable("dbo.TurmaAlunoes");
            DropTable("dbo.DisciplinaProfessors");
            DropTable("dbo.CursoDisciplinas");
            DropTable("dbo.ProvaAlunoes");
            DropTable("dbo.Configuracoes");
            DropTable("dbo.Opcoes");
            DropTable("dbo.Questoes");
            DropTable("dbo.Alternativas");
            DropTable("dbo.RespostasAlunos");
            DropTable("dbo.Turmas");
            DropTable("dbo.Cursos");
            DropTable("dbo.Disciplinas");
            DropTable("dbo.Professores");
            DropTable("dbo.ConfigsPln");
            DropTable("dbo.Provas");
            DropTable("dbo.Alunos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Admins");
        }
    }
}
