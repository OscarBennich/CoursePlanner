namespace CoursePlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Dob = c.DateTime(nullable: false),
                        ContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeacherContract", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.TeacherContract",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPercentageFall = c.Single(nullable: false),
                        TotalPercentageSpring = c.Single(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherReduction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        Term = c.Int(nullable: false),
                        Percentage = c.Single(nullable: false),
                        ContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeacherContract", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TeacherReduction", new[] { "ContractId" });
            DropIndex("dbo.Teacher", new[] { "ContractId" });
            DropForeignKey("dbo.TeacherReduction", "ContractId", "dbo.TeacherContract");
            DropForeignKey("dbo.Teacher", "ContractId", "dbo.TeacherContract");
            DropTable("dbo.TeacherReduction");
            DropTable("dbo.TeacherContract");
            DropTable("dbo.Teacher");
        }
    }
}
