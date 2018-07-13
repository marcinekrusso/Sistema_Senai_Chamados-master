namespace Senai.Chamados.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizacao_Tabela_Usuario_26_06_2018 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioDomains", newName: "Usuarios");
            AlterColumn("dbo.Usuarios", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Senha", c => c.String(nullable: false, maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "Senha", c => c.String());
            AlterColumn("dbo.Usuarios", "Email", c => c.String());
            AlterColumn("dbo.Usuarios", "Nome", c => c.String());
            RenameTable(name: "dbo.Usuarios", newName: "UsuarioDomains");
        }
    }
}
