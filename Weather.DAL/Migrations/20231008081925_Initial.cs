using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Weather.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Temperature = table.Column<double>(type: "double precision", nullable: true),
                    RelativeHumidity = table.Column<double>(type: "double precision", nullable: true),
                    DewPoint = table.Column<double>(type: "double precision", nullable: true),
                    AtmosphericPressure = table.Column<double>(type: "double precision", nullable: true),
                    WindDirection = table.Column<string>(type: "text", nullable: true),
                    WindSpeed = table.Column<double>(type: "double precision", nullable: true),
                    Cloudiness = table.Column<double>(type: "double precision", nullable: true),
                    CloudBaseHeight = table.Column<double>(type: "double precision", nullable: true),
                    Visibility = table.Column<double>(type: "double precision", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherRecords");
        }
    }
}
