using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NaxexUDemo.Data.Migrations
{
    public partial class userCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourceCapacity",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseCapacity",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnrolledCredits",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseCapacity",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrolledCredits",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CourceCapacity",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }
    }
}
