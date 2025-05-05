using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyMetaCore.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLive = table.Column<bool>(type: "bit", nullable: false),
                    IsPaused = table.Column<bool>(type: "bit", nullable: false),
                    IsAbandoned = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    StatusReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    TargetAudience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxResponses = table.Column<int>(type: "int", nullable: false),
                    QuotaLimit = table.Column<int>(type: "int", nullable: false),
                    QuotaCriteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentQuotaCount = table.Column<int>(type: "int", nullable: false),
                    IsQuotaFulfilled = table.Column<bool>(type: "bit", nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomCSS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WelcomeMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThankYouMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordProtection = table.Column<bool>(type: "bit", nullable: false),
                    EncryptionEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SupportedLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageSelectorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    HasIncentives = table.Column<bool>(type: "bit", nullable: false),
                    IncentiveType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncentiveValue = table.Column<double>(type: "float", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowMultipleAttempts = table.Column<bool>(type: "bit", nullable: false),
                    ShowProgressBar = table.Column<bool>(type: "bit", nullable: false),
                    TotalResponses = table.Column<int>(type: "int", nullable: false),
                    AvgCompletionTime = table.Column<double>(type: "float", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
