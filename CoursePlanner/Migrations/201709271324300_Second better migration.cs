namespace CoursePlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Secondbettermigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teacher", "ContractId", "dbo.TeacherContract");
            DropForeignKey("dbo.TeacherReduction", "ContractId", "dbo.TeacherContract");
            DropIndex("dbo.Teacher", new[] { "ContractId" });
            DropIndex("dbo.TeacherReduction", new[] { "ContractId" });
            AddColumn("dbo.Teacher", "TeacherContract_Id", c => c.Int());
            AddColumn("dbo.TeacherReduction", "TeacherContract_Id", c => c.Int());
            AddForeignKey("dbo.Teacher", "TeacherContract_Id", "dbo.TeacherContract", "Id");
            AddForeignKey("dbo.TeacherReduction", "TeacherContract_Id", "dbo.TeacherContract", "Id");
            CreateIndex("dbo.Teacher", "TeacherContract_Id");
            CreateIndex("dbo.TeacherReduction", "TeacherContract_Id");
            DropColumn("dbo.Teacher", "ContractId");
            DropColumn("dbo.TeacherReduction", "ContractId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherReduction", "ContractId", c => c.Int(nullable: false));
            AddColumn("dbo.Teacher", "ContractId", c => c.Int(nullable: false));
            DropIndex("dbo.TeacherReduction", new[] { "TeacherContract_Id" });
            DropIndex("dbo.Teacher", new[] { "TeacherContract_Id" });
            DropForeignKey("dbo.TeacherReduction", "TeacherContract_Id", "dbo.TeacherContract");
            DropForeignKey("dbo.Teacher", "TeacherContract_Id", "dbo.TeacherContract");
            DropColumn("dbo.TeacherReduction", "TeacherContract_Id");
            DropColumn("dbo.Teacher", "TeacherContract_Id");
            CreateIndex("dbo.TeacherReduction", "ContractId");
            CreateIndex("dbo.Teacher", "ContractId");
            AddForeignKey("dbo.TeacherReduction", "ContractId", "dbo.TeacherContract", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Teacher", "ContractId", "dbo.TeacherContract", "Id", cascadeDelete: true);
        }
    }
}
