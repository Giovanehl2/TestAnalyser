namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste212 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Matricula = c.Int(nullable: false),
                        Nome = c.String(unicode: false),
                        CPF = c.String(unicode: false),
                        Email = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Alternativas",
                c => new
                    {
                        AlternativaId = c.Int(nullable: false, identity: true),
                        DescAlternativa = c.String(unicode: false),
                        correto = c.Int(nullable: false),
                        Questao_QuestaoId = c.Int(),
                    })
                .PrimaryKey(t => t.AlternativaId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .Index(t => t.Questao_QuestaoId);
            
            CreateTable(
                "dbo.Questoes",
                c => new
                    {
                        QuestaoId = c.Int(nullable: false, identity: true),
                        Assunto = c.String(unicode: false),
                        Enunciado = c.String(unicode: false, storeType: "text"),
                        TipoQuestao = c.Int(nullable: false),
                        situacao = c.Int(nullable: false),
                        RespostaDiscursiva = c.String(unicode: false, storeType: "text"),
                        Disciplina_DisciplinaId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestaoId)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId)
                .Index(t => t.Disciplina_DisciplinaId);
            
            CreateTable(
                "dbo.Disciplinas",
                c => new
                    {
                        DisciplinaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.DisciplinaId);
            
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        AlunoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        CPF = c.String(unicode: false),
                        Matricula = c.Int(nullable: false),
                        Email = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AlunoId);
            
            CreateTable(
                "dbo.Turmas",
                c => new
                    {
                        TurmaId = c.Int(nullable: false, identity: true),
                        NomeTurma = c.String(unicode: false),
                        Periodo = c.Int(nullable: false),
                        Curso_CursoId = c.Int(),
                    })
                .PrimaryKey(t => t.TurmaId)
                .ForeignKey("dbo.Cursos", t => t.Curso_CursoId)
                .Index(t => t.Curso_CursoId);
            
            CreateTable(
                "dbo.Cursos",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity: true),
                        NomeCurso = c.String(unicode: false),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CursoId);
            
            CreateTable(
                "dbo.Professores",
                c => new
                    {
                        ProfessorId = c.Int(nullable: false, identity: true),
                        Matricula = c.Int(nullable: false),
                        Nome = c.String(unicode: false),
                        CPF = c.String(unicode: false),
                        Email = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ProfessorId);
            
            CreateTable(
                "dbo.Provas",
                c => new
                    {
                        ProvaId = c.Int(nullable: false, identity: true),
                        TituloProva = c.String(nullable: false, unicode: false),
                        QtSimplesEscolha = c.Int(nullable: false),
                        QtMultiplaEscolha = c.Int(nullable: false),
                        QtDissertativa = c.Int(nullable: false),
                        QtVerdadeirFalso = c.Int(nullable: false),
                        ValorProva = c.Double(nullable: false),
                        NotaProva = c.Double(nullable: false),
                        StatusProva = c.Int(nullable: false),
                        QtTentativas = c.Int(nullable: false),
                        DataProvaInicio = c.DateTime(nullable: false, precision: 0),
                        DataProvaFim = c.DateTime(nullable: false, precision: 0),
                        HoraInicio = c.DateTime(nullable: false, precision: 0),
                        HoraFim = c.DateTime(nullable: false, precision: 0),
                        ConfigPln_ConfigPlnId = c.Int(),
                        Disciplina_DisciplinaId = c.Int(),
                        Professor_ProfessorId = c.Int(),
                    })
                .PrimaryKey(t => t.ProvaId)
                .ForeignKey("dbo.ConfigsPln", t => t.ConfigPln_ConfigPlnId)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId)
                .ForeignKey("dbo.Professores", t => t.Professor_ProfessorId)
                .Index(t => t.ConfigPln_ConfigPlnId)
                .Index(t => t.Disciplina_DisciplinaId)
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
                "dbo.NotasQuestoes",
                c => new
                    {
                        NotaQuestaoId = c.Int(nullable: false, identity: true),
                        ValorQuestao = c.Double(nullable: false),
                        Questao_QuestaoId = c.Int(),
                        Prova_ProvaId = c.Int(),
                    })
                .PrimaryKey(t => t.NotaQuestaoId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .ForeignKey("dbo.Provas", t => t.Prova_ProvaId)
                .Index(t => t.Questao_QuestaoId)
                .Index(t => t.Prova_ProvaId);
            
            CreateTable(
                "dbo.RespostasAlunos",
                c => new
                    {
                        RespostasAlunoId = c.Int(nullable: false, identity: true),
                        RespostaDiscursiva = c.String(unicode: false),
                        NotaAluno = c.Double(nullable: false),
                        SituacaoCorrecao = c.Int(nullable: false),
                        DataHoraInicio = c.DateTime(precision: 0),
                        DataHoraFim = c.DateTime(precision: 0),
                        Aluno_AlunoId = c.Int(),
                        Questao_QuestaoId = c.Int(),
                        Prova_ProvaId = c.Int(),
                    })
                .PrimaryKey(t => t.RespostasAlunoId)
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .ForeignKey("dbo.Provas", t => t.Prova_ProvaId)
                .Index(t => t.Aluno_AlunoId)
                .Index(t => t.Questao_QuestaoId)
                .Index(t => t.Prova_ProvaId);
            
            CreateTable(
                "dbo.Opcoes",
                c => new
                    {
                        OpcaoId = c.Int(nullable: false, identity: true),
                        descricao = c.String(unicode: false),
                        Questao_QuestaoId = c.Int(),
                    })
                .PrimaryKey(t => t.OpcaoId)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId)
                .Index(t => t.Questao_QuestaoId);
            
            CreateTable(
                "dbo.AlunoNotas",
                c => new
                    {
                        AlunoNotaId = c.Int(nullable: false, identity: true),
                        NotaTotal = c.Double(nullable: false),
                        Aluno_AlunoId = c.Int(),
                        Prova_ProvaId = c.Int(),
                    })
                .PrimaryKey(t => t.AlunoNotaId)
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId)
                .ForeignKey("dbo.Provas", t => t.Prova_ProvaId)
                .Index(t => t.Aluno_AlunoId)
                .Index(t => t.Prova_ProvaId);
            
            CreateTable(
                "dbo.Configuracoes",
                c => new
                    {
                        ConfiguracaoId = c.Int(nullable: false, identity: true),
                        UrlApi = c.String(unicode: false),
                        sincAuto = c.String(unicode: false),
                        HoraCorrecao = c.String(unicode: false),
                        Instituicao = c.String(unicode: false),
                        Cnpj = c.String(unicode: false),
                        ServidorEmail = c.String(unicode: false),
                        CaixaSaida = c.String(unicode: false),
                        Senha = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ConfiguracaoId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Login = c.String(unicode: false),
                        Senha = c.String(unicode: false),
                        TipoUsr = c.Int(nullable: false),
                        Admin_AdminId = c.Int(),
                        Aluno_AlunoId = c.Int(),
                        Professor_ProfessorId = c.Int(),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Admins", t => t.Admin_AdminId)
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId)
                .ForeignKey("dbo.Professores", t => t.Professor_ProfessorId)
                .Index(t => t.Admin_AdminId)
                .Index(t => t.Aluno_AlunoId)
                .Index(t => t.Professor_ProfessorId);
            
            CreateTable(
                "dbo.AlunoDisciplinas",
                c => new
                    {
                        Aluno_AlunoId = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Aluno_AlunoId, t.Disciplina_DisciplinaId })
                .ForeignKey("dbo.Alunos", t => t.Aluno_AlunoId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId, cascadeDelete: true)
                .Index(t => t.Aluno_AlunoId)
                .Index(t => t.Disciplina_DisciplinaId);
            
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
            
            CreateTable(
                "dbo.ProfessorDisciplinas",
                c => new
                    {
                        Professor_ProfessorId = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_ProfessorId, t.Disciplina_DisciplinaId })
                .ForeignKey("dbo.Professores", t => t.Professor_ProfessorId, cascadeDelete: true)
                .ForeignKey("dbo.Disciplinas", t => t.Disciplina_DisciplinaId, cascadeDelete: true)
                .Index(t => t.Professor_ProfessorId)
                .Index(t => t.Disciplina_DisciplinaId);
            
            CreateTable(
                "dbo.RespostasAlunoAlternativas",
                c => new
                    {
                        RespostasAluno_RespostasAlunoId = c.Int(nullable: false),
                        Alternativa_AlternativaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RespostasAluno_RespostasAlunoId, t.Alternativa_AlternativaId })
                .ForeignKey("dbo.RespostasAlunos", t => t.RespostasAluno_RespostasAlunoId, cascadeDelete: true)
                .ForeignKey("dbo.Alternativas", t => t.Alternativa_AlternativaId, cascadeDelete: true)
                .Index(t => t.RespostasAluno_RespostasAlunoId)
                .Index(t => t.Alternativa_AlternativaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "Professor_ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.Usuarios", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.Usuarios", "Admin_AdminId", "dbo.Admins");
            DropForeignKey("dbo.AlunoNotas", "Prova_ProvaId", "dbo.Provas");
            DropForeignKey("dbo.AlunoNotas", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.Opcoes", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.Questoes", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.RespostasAlunos", "Prova_ProvaId", "dbo.Provas");
            DropForeignKey("dbo.RespostasAlunos", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.RespostasAlunos", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.RespostasAlunoAlternativas", "Alternativa_AlternativaId", "dbo.Alternativas");
            DropForeignKey("dbo.RespostasAlunoAlternativas", "RespostasAluno_RespostasAlunoId", "dbo.RespostasAlunos");
            DropForeignKey("dbo.Provas", "Professor_ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.NotasQuestoes", "Prova_ProvaId", "dbo.Provas");
            DropForeignKey("dbo.NotasQuestoes", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.Provas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.Provas", "ConfigPln_ConfigPlnId", "dbo.ConfigsPln");
            DropForeignKey("dbo.ProfessorDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.ProfessorDisciplinas", "Professor_ProfessorId", "dbo.Professores");
            DropForeignKey("dbo.TurmaDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.TurmaDisciplinas", "Turma_TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.Turmas", "Curso_CursoId", "dbo.Cursos");
            DropForeignKey("dbo.CursoDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.CursoDisciplinas", "Curso_CursoId", "dbo.Cursos");
            DropForeignKey("dbo.TurmaAlunoes", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.TurmaAlunoes", "Turma_TurmaId", "dbo.Turmas");
            DropForeignKey("dbo.AlunoDisciplinas", "Disciplina_DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.AlunoDisciplinas", "Aluno_AlunoId", "dbo.Alunos");
            DropForeignKey("dbo.Alternativas", "Questao_QuestaoId", "dbo.Questoes");
            DropIndex("dbo.RespostasAlunoAlternativas", new[] { "Alternativa_AlternativaId" });
            DropIndex("dbo.RespostasAlunoAlternativas", new[] { "RespostasAluno_RespostasAlunoId" });
            DropIndex("dbo.ProfessorDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.ProfessorDisciplinas", new[] { "Professor_ProfessorId" });
            DropIndex("dbo.TurmaDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.TurmaDisciplinas", new[] { "Turma_TurmaId" });
            DropIndex("dbo.CursoDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.CursoDisciplinas", new[] { "Curso_CursoId" });
            DropIndex("dbo.TurmaAlunoes", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.TurmaAlunoes", new[] { "Turma_TurmaId" });
            DropIndex("dbo.AlunoDisciplinas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.AlunoDisciplinas", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.Usuarios", new[] { "Professor_ProfessorId" });
            DropIndex("dbo.Usuarios", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.Usuarios", new[] { "Admin_AdminId" });
            DropIndex("dbo.AlunoNotas", new[] { "Prova_ProvaId" });
            DropIndex("dbo.AlunoNotas", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.Opcoes", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.RespostasAlunos", new[] { "Prova_ProvaId" });
            DropIndex("dbo.RespostasAlunos", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.RespostasAlunos", new[] { "Aluno_AlunoId" });
            DropIndex("dbo.NotasQuestoes", new[] { "Prova_ProvaId" });
            DropIndex("dbo.NotasQuestoes", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.Provas", new[] { "Professor_ProfessorId" });
            DropIndex("dbo.Provas", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.Provas", new[] { "ConfigPln_ConfigPlnId" });
            DropIndex("dbo.Turmas", new[] { "Curso_CursoId" });
            DropIndex("dbo.Questoes", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.Alternativas", new[] { "Questao_QuestaoId" });
            DropTable("dbo.RespostasAlunoAlternativas");
            DropTable("dbo.ProfessorDisciplinas");
            DropTable("dbo.TurmaDisciplinas");
            DropTable("dbo.CursoDisciplinas");
            DropTable("dbo.TurmaAlunoes");
            DropTable("dbo.AlunoDisciplinas");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Configuracoes");
            DropTable("dbo.AlunoNotas");
            DropTable("dbo.Opcoes");
            DropTable("dbo.RespostasAlunos");
            DropTable("dbo.NotasQuestoes");
            DropTable("dbo.ConfigsPln");
            DropTable("dbo.Provas");
            DropTable("dbo.Professores");
            DropTable("dbo.Cursos");
            DropTable("dbo.Turmas");
            DropTable("dbo.Alunos");
            DropTable("dbo.Disciplinas");
            DropTable("dbo.Questoes");
            DropTable("dbo.Alternativas");
            DropTable("dbo.Admins");
        }
    }
}
