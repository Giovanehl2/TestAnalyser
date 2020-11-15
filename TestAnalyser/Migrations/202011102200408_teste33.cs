namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste33 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assuntos",
                c => new
                    {
                        AssuntoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AssuntoId);
            
            AddColumn("dbo.Questoes", "AssuntoQuestao_AssuntoId", c => c.Int());
            AddColumn("dbo.Alunos", "NomeAluno", c => c.String(unicode: false));
            CreateIndex("dbo.Questoes", "AssuntoQuestao_AssuntoId");
            AddForeignKey("dbo.Questoes", "AssuntoQuestao_AssuntoId", "dbo.Assuntos", "AssuntoId");
            DropColumn("dbo.Questoes", "Assunto");
            DropColumn("dbo.Alunos", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alunos", "Nome", c => c.String(unicode: false));
            AddColumn("dbo.Questoes", "Assunto", c => c.String(unicode: false));
            DropForeignKey("dbo.Questoes", "AssuntoQuestao_AssuntoId", "dbo.Assuntos");
            DropIndex("dbo.Questoes", new[] { "AssuntoQuestao_AssuntoId" });
            DropColumn("dbo.Alunos", "NomeAluno");
            DropColumn("dbo.Questoes", "AssuntoQuestao_AssuntoId");
            DropTable("dbo.Assuntos");
        }
    }
}
