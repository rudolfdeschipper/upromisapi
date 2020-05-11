using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace upromiscontractapi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ContractType = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ContractType = table.Column<int>(nullable: false),
                    RequestType = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ContractType = table.Column<int>(nullable: false),
                    RequestType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractPaymentInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PlannedInvoiceDate = table.Column<DateTime>(nullable: false),
                    ActualInvoiceDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPaymentInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractPaymentInfo_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    MemberType = table.Column<int>(nullable: false),
                    Startdate = table.Column<DateTime>(nullable: true),
                    Enddate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractTeamComposition_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalPaymentInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Comment = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalPaymentInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProposalPaymentInfo_Proposals_ProposalID",
                        column: x => x.ProposalID,
                        principalTable: "Proposals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalID = table.Column<int>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    MemberType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProposalTeamComposition_Proposals_ProposalID",
                        column: x => x.ProposalID,
                        principalTable: "Proposals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    MemberType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestTeamComposition_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractPaymentInfo_ContractID",
                table: "ContractPaymentInfo",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTeamComposition_ContractID",
                table: "ContractTeamComposition",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalPaymentInfo_ProposalID",
                table: "ProposalPaymentInfo",
                column: "ProposalID");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalTeamComposition_ProposalID",
                table: "ProposalTeamComposition",
                column: "ProposalID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTeamComposition_RequestID",
                table: "RequestTeamComposition",
                column: "RequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractPaymentInfo");

            migrationBuilder.DropTable(
                name: "ContractTeamComposition");

            migrationBuilder.DropTable(
                name: "ProposalPaymentInfo");

            migrationBuilder.DropTable(
                name: "ProposalTeamComposition");

            migrationBuilder.DropTable(
                name: "RequestTeamComposition");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
