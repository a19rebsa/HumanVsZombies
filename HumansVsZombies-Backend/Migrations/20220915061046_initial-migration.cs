﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumansVsZombies_Backend.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GameState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NwLat = table.Column<double>(type: "float", nullable: true),
                    NwLng = table.Column<double>(type: "float", nullable: true),
                    SeLat = table.Column<double>(type: "float", nullable: true),
                    SeLng = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsHumanVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsZombieVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.MissionId);
                    table.ForeignKey(
                        name: "FK_Mission_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Squad",
                columns: table => new
                {
                    SquadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SquadName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsHuman = table.Column<bool>(type: "bit", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squad", x => x.SquadId);
                    table.ForeignKey(
                        name: "FK_Squad_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHuman = table.Column<bool>(type: "bit", nullable: false),
                    IsPatientZero = table.Column<bool>(type: "bit", nullable: false),
                    BiteCode = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    IsHumanGlobal = table.Column<bool>(type: "bit", nullable: false),
                    IsZombieGlobal = table.Column<bool>(type: "bit", nullable: false),
                    ChatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chat_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_Squad_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squad",
                        principalColumn: "SquadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kill",
                columns: table => new
                {
                    KillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfDeath = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lng = table.Column<double>(type: "float", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    KillerId = table.Column<int>(type: "int", nullable: false),
                    VictimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kill", x => x.KillId);
                    table.ForeignKey(
                        name: "FK_Kill_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId");
                    table.ForeignKey(
                        name: "FK_Kill_Player_KillerId",
                        column: x => x.KillerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_Kill_Player_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateTable(
                name: "SquadMember",
                columns: table => new
                {
                    SquadMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadMember", x => x.SquadMemberId);
                    table.ForeignKey(
                        name: "FK_SquadMember_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquadMember_Squad_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squad",
                        principalColumn: "SquadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SquadCheckin",
                columns: table => new
                {
                    SquadCheckinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: false),
                    SquadMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadCheckin", x => x.SquadCheckinId);
                    table.ForeignKey(
                        name: "FK_SquadCheckin_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquadCheckin_Squad_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squad",
                        principalColumn: "SquadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SquadCheckin_SquadMember_SquadMemberId",
                        column: x => x.SquadMemberId,
                        principalTable: "SquadMember",
                        principalColumn: "SquadMemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Description", "GameName", "GameState", "NwLat", "NwLng", "SeLat", "SeLng" },
                values: new object[,]
                {
                    { 1, "Intresting game", "Left for Dead", "Registration", -26.66386, 25.283757999999999, -16.66686, 17.96686 },
                    { 2, "Absorbing game", "Walking Dead", "In progress", -16.66386, 15.283758000000001, -6.6668599999999998, 7.9668599999999996 },
                    { 3, "Fascinating game", "Days Gone", "Complete", -20.263860000000001, 21.283358, -13.66686, 12.99686 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "FirstName", "IsAdmin", "LastName" },
                values: new object[,]
                {
                    { "c208f8b8-22bb-464c-93c9-0e011f4d827f", "Rebecka", false, "Ocampo Sandgren" },
                    { "c151a086-fe63-41ed-9fa0-1dcb43f7a556", "Fadi", true, "Akkaoui" },
                    { "3885542f-5e69-4493-aef8-69a55ec152a3", "Negin", false, "Bakhtiarirad" },
                    { "c8f391c6-2257-4ed0-bef2-f275bff089d2", "Betiel", false, "Yohannes" }
                });

            migrationBuilder.InsertData(
                table: "Mission",
                columns: new[] { "MissionId", "Description", "EndTime", "GameId", "IsHumanVisible", "IsZombieVisible", "MissionName", "StartTime" },
                values: new object[,]
                {
                    { 1, "Try your best to collect five types of medicine. Good Luck!", new DateTime(2022, 11, 30, 18, 32, 20, 0, DateTimeKind.Unspecified), 1, true, false, "Collect medicine", new DateTime(2022, 11, 30, 17, 32, 20, 0, DateTimeKind.Unspecified) },
                    { 2, "Try your best to collect five types of powerpotion. Good Luck!", new DateTime(2022, 11, 30, 15, 32, 20, 0, DateTimeKind.Unspecified), 1, false, true, "Collect powerpotion", new DateTime(2022, 11, 30, 14, 32, 20, 0, DateTimeKind.Unspecified) },
                    { 3, "Try your best to collect as many weapons as possible. Good Luck!", new DateTime(2022, 11, 30, 21, 32, 20, 0, DateTimeKind.Unspecified), 2, true, false, "Collect weapons", new DateTime(2022, 11, 30, 20, 32, 20, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "BiteCode", "GameId", "IsHuman", "IsPatientZero", "UserId" },
                values: new object[,]
                {
                    { 1, 23234, 1, true, false, "c208f8b8-22bb-464c-93c9-0e011f4d827f" },
                    { 2, 17154, 1, false, true, "c151a086-fe63-41ed-9fa0-1dcb43f7a556" },
                    { 3, 10911, 2, false, false, "3885542f-5e69-4493-aef8-69a55ec152a3" },
                    { 4, 18737, 3, true, false, "c8f391c6-2257-4ed0-bef2-f275bff089d2" }
                });

            migrationBuilder.InsertData(
                table: "Squad",
                columns: new[] { "SquadId", "GameId", "IsHuman", "SquadName" },
                values: new object[,]
                {
                    { 1, 1, true, "Best squad ever" },
                    { 3, 1, false, "Gang gang" },
                    { 2, 2, false, "Better than best squad" },
                    { 4, 3, true, "The beasts" }
                });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "ChatId", "ChatTime", "GameId", "IsHumanGlobal", "IsZombieGlobal", "Message", "PlayerId", "SquadId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 15, 8, 10, 46, 68, DateTimeKind.Local).AddTicks(6533), 1, false, false, "Who is the zombie today?", 1, null },
                    { 2, new DateTime(2022, 9, 15, 8, 10, 46, 68, DateTimeKind.Local).AddTicks(7004), 1, true, false, "Hello", 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Kill",
                columns: new[] { "KillId", "GameId", "KillerId", "Lat", "Lng", "Story", "TimeOfDeath", "VictimId" },
                values: new object[] { 1, 1, 1, -24.66206, 15.213158, "The zombie tagged the Human when she was eating", new DateTime(2022, 10, 30, 14, 32, 21, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "SquadMember",
                columns: new[] { "SquadMemberId", "PlayerId", "Rank", "SquadId" },
                values: new object[,]
                {
                    { 4, 1, 10, 4 },
                    { 2, 2, 5, 2 },
                    { 1, 3, 4, 1 },
                    { 3, 4, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "SquadCheckin",
                columns: new[] { "SquadCheckinId", "EndTime", "GameId", "Lat", "Lng", "SquadId", "SquadMemberId", "StartTime" },
                values: new object[] { 2, new DateTime(2022, 9, 15, 8, 20, 46, 67, DateTimeKind.Local).AddTicks(8029), 2, -26.66386, 25.283757999999999, 2, 2, new DateTime(2022, 9, 15, 8, 10, 46, 67, DateTimeKind.Local).AddTicks(8021) });

            migrationBuilder.InsertData(
                table: "SquadCheckin",
                columns: new[] { "SquadCheckinId", "EndTime", "GameId", "Lat", "Lng", "SquadId", "SquadMemberId", "StartTime" },
                values: new object[] { 3, new DateTime(2022, 9, 15, 8, 20, 46, 67, DateTimeKind.Local).AddTicks(8035), 2, -26.66386, 25.283757999999999, 2, 2, new DateTime(2022, 9, 15, 8, 10, 46, 67, DateTimeKind.Local).AddTicks(8033) });

            migrationBuilder.InsertData(
                table: "SquadCheckin",
                columns: new[] { "SquadCheckinId", "EndTime", "GameId", "Lat", "Lng", "SquadId", "SquadMemberId", "StartTime" },
                values: new object[] { 1, new DateTime(2022, 9, 15, 8, 20, 46, 67, DateTimeKind.Local).AddTicks(6908), 1, -26.66386, 25.283757999999999, 1, 1, new DateTime(2022, 9, 15, 8, 10, 46, 65, DateTimeKind.Local).AddTicks(1723) });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_GameId",
                table: "Chat",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_PlayerId",
                table: "Chat",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_SquadId",
                table: "Chat",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_Kill_GameId",
                table: "Kill",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Kill_KillerId",
                table: "Kill",
                column: "KillerId");

            migrationBuilder.CreateIndex(
                name: "IX_Kill_VictimId",
                table: "Kill",
                column: "VictimId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mission_GameId",
                table: "Mission",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GameId",
                table: "Player",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserId",
                table: "Player",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Squad_GameId",
                table: "Squad",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadCheckin_GameId",
                table: "SquadCheckin",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadCheckin_SquadId",
                table: "SquadCheckin",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadCheckin_SquadMemberId",
                table: "SquadCheckin",
                column: "SquadMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadMember_PlayerId",
                table: "SquadMember",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SquadMember_SquadId",
                table: "SquadMember",
                column: "SquadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Kill");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "SquadCheckin");

            migrationBuilder.DropTable(
                name: "SquadMember");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Squad");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
