using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIQuim.Migrations
{
    /// <inheritdoc />
    public partial class AddPedidoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Registro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ResponsableEntregaId = table.Column<int>(type: "int", nullable: false),
                    ResponsableRecibeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedido__3214EC27", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pedido_Entrega",
                        column: x => x.ResponsableEntregaId,
                        principalTable: "Empleados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Recibe",
                        column: x => x.ResponsableRecibeId,
                        principalTable: "Empleados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registro_PedidoId",
                table: "Registro",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ResponsableEntregaId",
                table: "Pedido",
                column: "ResponsableEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ResponsableRecibeId",
                table: "Pedido",
                column: "ResponsableRecibeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registro_Pedido",
                table: "Registro",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registro_Pedido",
                table: "Registro");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Registro_PedidoId",
                table: "Registro");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Registro");
        }
    }
}
