namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TurmaAlunoes", newName: "AlunoTurmas");
            RenameTable(name: "dbo.DisciplinaProfessors", newName: "ProfessorDisciplinas");
            DropForeignKey("dbo.Questoes", "Prova_ProvaId", "dbo.Provas");
            DropIndex("dbo.Questoes", new[] { "Prova_ProvaId" });
            DropPrimaryKey("dbo.AlunoTurmas");
            DropPrimaryKey("dbo.ProfessorDisciplinas");
            CreateTable(
                "dbo.ProvaQuestaos",
                c => new
                    {
                        Prova_ProvaId = c.Int(nullable: false),
                        Questao_QuestaoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Prova_ProvaId, t.Questao_QuestaoId })
                .ForeignKey("dbo.Provas", t => t.Prova_ProvaId, cascadeDelete: true)
                .ForeignKey("dbo.Questoes", t => t.Questao_QuestaoId, cascadeDelete: true)
                .Index(t => t.Prova_ProvaId)
                .Index(t => t.Questao_QuestaoId);
            
            AlterColumn("dbo.Questoes", "RespostaDiscursiva", c => c.String(unicode: false));
            AddPrimaryKey("dbo.AlunoTurmas", new[] { "Aluno_AlunoId", "Turma_TurmaId" });
            AddPrimaryKey("dbo.ProfessorDisciplinas", new[] { "Professor_ProfessorId", "Disciplina_DisciplinaId" });
            DropColumn("dbo.Questoes", "Prova_ProvaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questoes", "Prova_ProvaId", c => c.Int());
            DropForeignKey("dbo.ProvaQuestaos", "Questao_QuestaoId", "dbo.Questoes");
            DropForeignKey("dbo.ProvaQuestaos", "Prova_ProvaId", "dbo.Provas");
            DropIndex("dbo.ProvaQuestaos", new[] { "Questao_QuestaoId" });
            DropIndex("dbo.ProvaQuestaos", new[] { "Prova_ProvaId" });
            DropPrimaryKey("dbo.ProfessorDisciplinas");
            DropPrimaryKey("dbo.AlunoTurmas");
            AlterColumn("dbo.Questoes", "RespostaDiscursiva", c => c.String());
            DropTable("dbo.ProvaQuestaos");
            AddPrimaryKey("dbo.ProfessorDisciplinas", new[] { "Disciplina_DisciplinaId", "Professor_ProfessorId" });
            AddPrimaryKey("dbo.AlunoTurmas", new[] { "Turma_TurmaId", "Aluno_AlunoId" });
            CreateIndex("dbo.Questoes", "Prova_ProvaId");
            AddForeignKey("dbo.Questoes", "Prova_ProvaId", "dbo.Provas", "ProvaId");
            RenameTable(name: "dbo.ProfessorDisciplinas", newName: "DisciplinaProfessors");
            RenameTable(name: "dbo.AlunoTurmas", newName: "TurmaAlunoes");
        }
    }
}
