namespace Question2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Mid = c.Long(nullable: false, identity: true),
                        Moviename = c.String(),
                        DirectorName = c.String(),
                        DateofRelease = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Mid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
        }
    }
}
