using Microsoft.EntityFrameworkCore.Migrations;

namespace todo_domain_entities.Migrations
{
    public partial class AddhiddeninList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_TodoLists_ToDoListId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_ToDoListId",
                table: "TodoItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ToDoListId",
                table: "TodoItems",
                column: "ToDoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_TodoLists_ToDoListId",
                table: "TodoItems",
                column: "ToDoListId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
