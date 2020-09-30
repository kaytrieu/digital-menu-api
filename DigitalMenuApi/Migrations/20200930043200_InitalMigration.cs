using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalMenuApi.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcountRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcountRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoxType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, nullable: true),
                    Description = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Src = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Src = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(unicode: false, nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AcountRole",
                        column: x => x.RoleId,
                        principalTable: "AcountRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Store",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Screen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
                    UId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screen_Store",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    StoreId = table.Column<int>(nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    UILink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_Store",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: true),
                    Location = table.Column<int>(nullable: true),
                    BoxTypeId = table.Column<int>(nullable: true),
                    Src = table.Column<string>(nullable: true),
                    HeaderId = table.Column<int>(nullable: true),
                    FooterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Box_ContainerType",
                        column: x => x.BoxTypeId,
                        principalTable: "BoxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Box_Session1",
                        column: x => x.FooterId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Box_Session",
                        column: x => x.HeaderId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Container_Template",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScreenTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenTemplate_Screen",
                        column: x => x.ScreenId,
                        principalTable: "Screen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreenTemplate_Template",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    BoxId = table.Column<int>(nullable: true),
                    MaxSize = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductList_Box",
                        column: x => x.BoxId,
                        principalTable: "Box",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductListProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductListId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Location = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductListProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductListProduct_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductListProduct_ProductList",
                        column: x => x.ProductListId,
                        principalTable: "ProductList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_StoreId",
                table: "Account",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_BoxTypeId",
                table: "Box",
                column: "BoxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_FooterId",
                table: "Box",
                column: "FooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_HeaderId",
                table: "Box",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_TemplateId",
                table: "Box",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductList_BoxId",
                table: "ProductList",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListProduct_ProductId",
                table: "ProductListProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListProduct_ProductListId",
                table: "ProductListProduct",
                column: "ProductListId");

            migrationBuilder.CreateIndex(
                name: "IX_Screen_StoreId",
                table: "Screen",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTemplate_ScreenId",
                table: "ScreenTemplate",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTemplate_TemplateId",
                table: "ScreenTemplate",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_StoreId",
                table: "Template",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ProductListProduct");

            migrationBuilder.DropTable(
                name: "ScreenTemplate");

            migrationBuilder.DropTable(
                name: "AcountRole");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductList");

            migrationBuilder.DropTable(
                name: "Screen");

            migrationBuilder.DropTable(
                name: "Box");

            migrationBuilder.DropTable(
                name: "BoxType");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
