using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Persistence.Migrations
{
    public partial class SeedDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Departments (Name) VALUES ('Marketing')");
            migrationBuilder.Sql("INSERT INTO Departments (Name) VALUES ('Accounting')");
            migrationBuilder.Sql("INSERT INTO Departments (Name) VALUES ('Human Resources')");
            migrationBuilder.Sql("INSERT INTO Departments (Name) VALUES ('Quality Control')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Departments WHERE Name IN ('Marketing', 'Accounting', 'Human Resources', 'Quality Control')");
        }
    }
}
