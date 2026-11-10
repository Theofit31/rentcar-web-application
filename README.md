# RentCar Web Application

A web-based car rental management application built using **ASP.NET Core MVC and C#**. This application allows customers to browse available vehicles, make rental bookings, and manage their rental history, while administrators can manage vehicles, customers, and rental transactions.

## Overview

RentCar is a web application designed to simplify the car rental process through a centralized platform. It provides separate functionalities for customers and administrators to manage the rental process efficiently.

## Features

### Customer Features
- **User Authentication** – Customer registration and login.
- **Vehicle Browsing** – View available rental vehicles and their details.
- **Vehicle Information** – View vehicle specifications, images, transmission types, seating capacity, and rental prices.
- **Rental Booking** – Submit car rental bookings.
- **Rental History** – View personal rental records and booking details.
- **Payment Page** – Access the rental payment interface.
- **Customer Profile** – View and manage customer profile information.

### Admin Features
- **Admin Dashboard** – View the rental management dashboard.
- **Vehicle Management** – Manage vehicle information and availability.
- **Customer Management** – View and manage customer records.
- **Transaction Management** – Monitor and manage rental transactions.

## Tech Stack

| Technology | Purpose |
|---|---|
| C# | Backend programming language |
| ASP.NET Core MVC (.NET 8) | Web application framework |
| Entity Framework Core | Object-relational mapping (ORM) |
| SQL Server / LocalDB | Database management |
| LINQ | Database querying |
| HTML | Web page structure |
| CSS | Styling and layout |
| JavaScript | Client-side functionality |
| Bootstrap | Responsive UI components |

## Project Structure

```text
RentCar/
├── RentCar.sln
├── .gitignore
└── RentCarWeb/
    ├── Controllers/
    │   ├── AdminController.cs
    │   ├── AuthController.cs
    │   ├── HomeController.cs
    │   └── RentalController.cs
    ├── Data/
    │   ├── RentCarDbContext.cs
    │   └── Seeders/
    │       └── DataSeeder.cs
    ├── Migrations/
    ├── Models/
    │   ├── ViewModels/
    │   └── ...
    ├── Properties/
    ├── Views/
    │   ├── Admin/
    │   ├── Auth/
    │   ├── Home/
    │   ├── Rental/
    │   └── Shared/
    ├── wwwroot/
    │   ├── css/
    │   ├── images/
    │   ├── js/
    │   └── lib/
    ├── appsettings.json
    ├── Program.cs
    └── RentCarWeb.csproj
```

## Getting Started

### Prerequisites

Make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or Visual Studio Code
- SQL Server LocalDB or another compatible SQL Server instance

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/Theofit31/rentcar-web-application.git
```

**2. Navigate to the project directory**

```bash
cd rentcar-web-application
```

**3. Restore NuGet packages**

```bash
dotnet restore
```

**4. Configure the database connection**

Open `RentCarWeb/appsettings.json` and update the connection string if necessary.

The default configuration uses SQL Server LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=RentCarDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Make sure your SQL Server instance is available before proceeding.

**5. Apply database migrations**

```bash
dotnet ef database update --project RentCarWeb
```

If the Entity Framework Core CLI is not installed, install it first:

```bash
dotnet tool install --global dotnet-ef
```

**6. Run the application**

```bash
dotnet run --project RentCarWeb
```

Open the local URL displayed in the terminal to access the application.

## Database

The application uses SQL Server with Entity Framework Core for database operations.

The database context is implemented in:

```text
RentCarWeb/Data/RentCarDbContext.cs
```

Database migrations are available in:

```text
RentCarWeb/Migrations/
```

The project also includes a data seeder that initializes sample vehicle records.

## Author

**Theo Filus Iglesias Triadi**
*This project was developed as an individual web application project for the IT Division selection process.*
