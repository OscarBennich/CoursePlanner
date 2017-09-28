namespace CoursePlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherContract", "ContractType", c => c.Int(nullable: false));
            AddColumn("dbo.TeacherContract", "TemporaryContractEndDate", c => c.String());
            AlterColumn("dbo.Teacher", "Dob", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teacher", "Dob", c => c.DateTime(nullable: false));
            DropColumn("dbo.TeacherContract", "TemporaryContractEndDate");
            DropColumn("dbo.TeacherContract", "ContractType");
        }
    }
}
