using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PushThenPause.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels_Cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakActivities_Users_userId",
                table: "BreakActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cycles_BreakActivities_BreakActivityId",
                table: "Cycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Cycles_Tasks_TaskId",
                table: "Cycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Cycles_Users_userId",
                table: "Cycles");

            migrationBuilder.DropForeignKey(
                name: "FK_NemsModeSettings_Users_userId",
                table: "NemsModeSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_StreakTrackers_Users_userId",
                table: "StreakTrackers");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_StreakTrackers_userId",
                table: "StreakTrackers");

            migrationBuilder.DropIndex(
                name: "IX_NemsModeSettings_userId",
                table: "NemsModeSettings");

            migrationBuilder.DropIndex(
                name: "IX_Cycles_BreakActivityId",
                table: "Cycles");

            migrationBuilder.DropIndex(
                name: "IX_Cycles_TaskId",
                table: "Cycles");

            migrationBuilder.DropIndex(
                name: "IX_Cycles_userId",
                table: "Cycles");

            migrationBuilder.DropIndex(
                name: "IX_BreakActivities_userId",
                table: "BreakActivities");

            migrationBuilder.DropColumn(
                name: "BreaksTaken",
                table: "StreakTrackers");

            migrationBuilder.DropColumn(
                name: "EndedAt",
                table: "Cycles");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "StreakTrackers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "StepsCompleted",
                table: "StreakTrackers",
                newName: "StreakCount");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "NemsModeSettings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Cycles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "Cycles",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "BreakActivities",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TaskCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutesBreakActivity",
                table: "Cycles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutesUserTask",
                table: "Cycles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    UserTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    DurationMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => x.UserTaskId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskCategories");

            migrationBuilder.DropColumn(
                name: "DurationMinutesBreakActivity",
                table: "Cycles");

            migrationBuilder.DropColumn(
                name: "DurationMinutesUserTask",
                table: "Cycles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StreakTrackers",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "StreakCount",
                table: "StreakTrackers",
                newName: "StepsCompleted");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "NemsModeSettings",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cycles",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Cycles",
                newName: "StartedAt");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BreakActivities",
                newName: "userId");

            migrationBuilder.AddColumn<int>(
                name: "BreaksTaken",
                table: "StreakTrackers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndedAt",
                table: "Cycles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    UserTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    DurationMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.UserTaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskCategories_TaskCategoryId",
                        column: x => x.TaskCategoryId,
                        principalTable: "TaskCategories",
                        principalColumn: "TaskCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StreakTrackers_userId",
                table: "StreakTrackers",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NemsModeSettings_userId",
                table: "NemsModeSettings",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cycles_BreakActivityId",
                table: "Cycles",
                column: "BreakActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cycles_TaskId",
                table: "Cycles",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Cycles_userId",
                table: "Cycles",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakActivities_userId",
                table: "BreakActivities",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskCategoryId",
                table: "Tasks",
                column: "TaskCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_userId",
                table: "Tasks",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakActivities_Users_userId",
                table: "BreakActivities",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cycles_BreakActivities_BreakActivityId",
                table: "Cycles",
                column: "BreakActivityId",
                principalTable: "BreakActivities",
                principalColumn: "BreakActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cycles_Tasks_TaskId",
                table: "Cycles",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "UserTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cycles_Users_userId",
                table: "Cycles",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NemsModeSettings_Users_userId",
                table: "NemsModeSettings",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StreakTrackers_Users_userId",
                table: "StreakTrackers",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
