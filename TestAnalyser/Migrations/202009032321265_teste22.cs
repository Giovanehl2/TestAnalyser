namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RespostasAlunos", "DataHoraInicio", c => c.DateTime());
            AlterColumn("dbo.RespostasAlunos", "DataHoraFim", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RespostasAlunos", "DataHoraFim", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RespostasAlunos", "DataHoraInicio", c => c.DateTime(nullable: false));
        }
    }
}
