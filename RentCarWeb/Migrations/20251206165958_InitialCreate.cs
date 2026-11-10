using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsCars",
                columns: table => new
                {
                    Car_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    License_Plate = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Number_Of_Car_Seats = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price_Per_Day = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCars", x => x.Car_Id);
                });

            migrationBuilder.CreateTable(
                name: "MsCustomers",
                columns: table => new
                {
                    Customer_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Driver_License_Number = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCustomers", x => x.Customer_Id);
                });

            migrationBuilder.CreateTable(
                name: "MsEmployees",
                columns: table => new
                {
                    Employee_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsEmployees", x => x.Employee_Id);
                });

            migrationBuilder.CreateTable(
                name: "MsCarImages",
                columns: table => new
                {
                    Image_Car_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Car_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Image_Link = table.Column<string>(type: "nvarchar(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCarImages", x => x.Image_Car_Id);
                    table.ForeignKey(
                        name: "FK_MsCarImages_MsCars_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "MsCars",
                        principalColumn: "Car_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrRentals",
                columns: table => new
                {
                    Rental_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Rental_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Payment_Status = table.Column<bool>(type: "bit", nullable: false),
                    Customer_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Car_Id = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrRentals", x => x.Rental_Id);
                    table.ForeignKey(
                        name: "FK_TrRentals_MsCars_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "MsCars",
                        principalColumn: "Car_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrRentals_MsCustomers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "MsCustomers",
                        principalColumn: "Customer_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrMaintenances",
                columns: table => new
                {
                    Maintenance_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Maintenance_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Car_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Employee_Id = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrMaintenances", x => x.Maintenance_Id);
                    table.ForeignKey(
                        name: "FK_TrMaintenances_MsCars_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "MsCars",
                        principalColumn: "Car_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrMaintenances_MsEmployees_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "MsEmployees",
                        principalColumn: "Employee_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LtPayments",
                columns: table => new
                {
                    Payment_Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Payment_Method = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Rental_Id = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LtPayments", x => x.Payment_Id);
                    table.ForeignKey(
                        name: "FK_LtPayments_TrRentals_Rental_Id",
                        column: x => x.Rental_Id,
                        principalTable: "TrRentals",
                        principalColumn: "Rental_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LtPayments_Rental_Id",
                table: "LtPayments",
                column: "Rental_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MsCarImages_Car_Id",
                table: "MsCarImages",
                column: "Car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenances_Car_Id",
                table: "TrMaintenances",
                column: "Car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenances_Employee_Id",
                table: "TrMaintenances",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrRentals_Car_Id",
                table: "TrRentals",
                column: "Car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrRentals_Customer_Id",
                table: "TrRentals",
                column: "Customer_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LtPayments");

            migrationBuilder.DropTable(
                name: "MsCarImages");

            migrationBuilder.DropTable(
                name: "TrMaintenances");

            migrationBuilder.DropTable(
                name: "TrRentals");

            migrationBuilder.DropTable(
                name: "MsEmployees");

            migrationBuilder.DropTable(
                name: "MsCars");

            migrationBuilder.DropTable(
                name: "MsCustomers");
        }
    }
}
