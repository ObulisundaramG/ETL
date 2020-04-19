using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ETL_Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfaRepository",
                columns: table => new
                {
                    RepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepName = table.Column<string>(nullable: true),
                    RepDesc = table.Column<string>(nullable: true),
                    DatabaseType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaRepository", x => x.RepId);
                });

            migrationBuilder.CreateTable(
                name: "InfaFolder",
                columns: table => new
                {
                    FldrId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepId = table.Column<int>(nullable: true),
                    FldrName = table.Column<string>(nullable: true),
                    FldrDesc = table.Column<string>(nullable: true),
                    Shared = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaFolder", x => x.FldrId);
                    table.ForeignKey(
                        name: "FK_InfaFolder_InfaRepository_RepId",
                        column: x => x.RepId,
                        principalTable: "InfaRepository",
                        principalColumn: "RepId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfaWorkflow",
                columns: table => new
                {
                    WkfId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FldrId = table.Column<int>(nullable: true),
                    WkfName = table.Column<string>(nullable: true),
                    WkfDesc = table.Column<string>(nullable: true),
                    WkfCol = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaWorkflow", x => x.WkfId);
                    table.ForeignKey(
                        name: "FK_InfaWorkflow_InfaFolder_FldrId",
                        column: x => x.FldrId,
                        principalTable: "InfaFolder",
                        principalColumn: "FldrId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfaSession",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WkfId = table.Column<int>(nullable: true),
                    SessionName = table.Column<string>(nullable: true),
                    SessionDesc = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaSession", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_InfaSession_InfaWorkflow_WkfId",
                        column: x => x.WkfId,
                        principalTable: "InfaWorkflow",
                        principalColumn: "WkfId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfaMapping",
                columns: table => new
                {
                    MappingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(nullable: true),
                    FldrId = table.Column<int>(nullable: true),
                    MappingName = table.Column<string>(nullable: true),
                    MappingDesc = table.Column<string>(nullable: true),
                    MappingIsvalid = table.Column<string>(nullable: true),
                    TransType = table.Column<string>(nullable: true),
                    TransName = table.Column<string>(nullable: true),
                    TransReusable = table.Column<string>(nullable: true),
                    FieldName = table.Column<string>(nullable: true),
                    FieldDesc = table.Column<string>(nullable: true),
                    Datatype = table.Column<string>(nullable: true),
                    Porttype = table.Column<string>(nullable: true),
                    Precision = table.Column<int>(nullable: true),
                    Scale = table.Column<int>(nullable: true),
                    Expression = table.Column<string>(nullable: true),
                    ExpressionType = table.Column<string>(nullable: true),
                    TableattributeName = table.Column<string>(nullable: true),
                    TableattributeValue = table.Column<string>(nullable: true),
                    SortKey = table.Column<string>(nullable: true),
                    SortDirection = table.Column<string>(nullable: true),
                    InstanceName = table.Column<string>(nullable: true),
                    InstanceType = table.Column<string>(nullable: true),
                    AssociatedSourceInstance = table.Column<string>(nullable: true),
                    TargetInstance = table.Column<string>(nullable: true),
                    TargetLoadorder = table.Column<string>(nullable: true),
                    Flag = table.Column<string>(nullable: true),
                    XMLTag = table.Column<string>(nullable: true),
                    LoadTimestamp = table.Column<DateTime>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaMapping", x => x.MappingId);
                    table.ForeignKey(
                        name: "FK_InfaMapping_InfaFolder_FldrId",
                        column: x => x.FldrId,
                        principalTable: "InfaFolder",
                        principalColumn: "FldrId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfaMapping_InfaSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "InfaSession",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfaSessionConfig",
                columns: table => new
                {
                    SessionConfigId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFlowId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    SessionName = table.Column<string>(nullable: true),
                    XMLTag = table.Column<string>(nullable: true),
                    ConfigAttributeName = table.Column<string>(nullable: true),
                    ConfigAttributeDescValue = table.Column<string>(nullable: true),
                    IsDefault = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaSessionConfig", x => x.SessionConfigId);
                    table.ForeignKey(
                        name: "FK_InfaSessionConfig_InfaSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "InfaSession",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfaSessionConfig_InfaWorkflow_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "InfaWorkflow",
                        principalColumn: "WkfId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfaConnector",
                columns: table => new
                {
                    ConnectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MappingId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    XmlTag = table.Column<string>(nullable: true),
                    FromField = table.Column<string>(nullable: true),
                    FromInstance = table.Column<string>(nullable: true),
                    FromInstanceType = table.Column<string>(nullable: true),
                    ToField = table.Column<string>(nullable: true),
                    ToInstance = table.Column<string>(nullable: true),
                    ToInstanceType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    InfaFolderFldrId = table.Column<int>(nullable: true),
                    InfaRepositoryRepId = table.Column<int>(nullable: true),
                    InfaWorkflowWkfId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfaConnector", x => x.ConnectorId);
                    table.ForeignKey(
                        name: "FK_InfaConnector_InfaFolder_InfaFolderFldrId",
                        column: x => x.InfaFolderFldrId,
                        principalTable: "InfaFolder",
                        principalColumn: "FldrId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfaConnector_InfaRepository_InfaRepositoryRepId",
                        column: x => x.InfaRepositoryRepId,
                        principalTable: "InfaRepository",
                        principalColumn: "RepId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfaConnector_InfaWorkflow_InfaWorkflowWkfId",
                        column: x => x.InfaWorkflowWkfId,
                        principalTable: "InfaWorkflow",
                        principalColumn: "WkfId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfaConnector_InfaMapping_MappingId",
                        column: x => x.MappingId,
                        principalTable: "InfaMapping",
                        principalColumn: "MappingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfaConnector_InfaSession_SessionId",
                        column: x => x.SessionId,
                        principalTable: "InfaSession",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfaConnector_InfaFolderFldrId",
                table: "InfaConnector",
                column: "InfaFolderFldrId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaConnector_InfaRepositoryRepId",
                table: "InfaConnector",
                column: "InfaRepositoryRepId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaConnector_InfaWorkflowWkfId",
                table: "InfaConnector",
                column: "InfaWorkflowWkfId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaConnector_MappingId",
                table: "InfaConnector",
                column: "MappingId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaConnector_SessionId",
                table: "InfaConnector",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaFolder_RepId",
                table: "InfaFolder",
                column: "RepId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaMapping_FldrId",
                table: "InfaMapping",
                column: "FldrId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaMapping_SessionId",
                table: "InfaMapping",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaSession_WkfId",
                table: "InfaSession",
                column: "WkfId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaSessionConfig_SessionId",
                table: "InfaSessionConfig",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaSessionConfig_WorkFlowId",
                table: "InfaSessionConfig",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_InfaWorkflow_FldrId",
                table: "InfaWorkflow",
                column: "FldrId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfaConnector");

            migrationBuilder.DropTable(
                name: "InfaSessionConfig");

            migrationBuilder.DropTable(
                name: "InfaMapping");

            migrationBuilder.DropTable(
                name: "InfaSession");

            migrationBuilder.DropTable(
                name: "InfaWorkflow");

            migrationBuilder.DropTable(
                name: "InfaFolder");

            migrationBuilder.DropTable(
                name: "InfaRepository");
        }
    }
}
