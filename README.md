# OnlineStoreMVC

An **ASP.NET Core MVC** web application for managing an online store's product catalog, built with **Entity Framework Core** and SQL Server. It uses Razor Views and Bootstrap for the UI.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-8.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white)

## Features

- **Product list**: view all products with name, brand, category, price, image and creation date.
- **Create product**: form with server-side validation (all fields required) and image upload. Images are saved to `wwwroot/products/` with a timestamp-based file name.
- **Edit product**: update product details and optionally replace the image.
- **Delete product**: removes the product from the database and deletes its image file.
- Data access through Entity Framework Core, with code-first migrations.

## Tech Stack

- ASP.NET Core MVC on .NET 8
- Entity Framework Core 8 (SQL Server provider, EF Core Tools)
- SQL Server / LocalDB
- Razor Views, Bootstrap, jQuery Validation

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server or SQL Server Express LocalDB
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Run locally

1. Clone the repository:

   ```bash
   git clone https://github.com/sharky412/OnlineStoreMVC.git
   cd OnlineStoreMVC
   ```

2. Check the `DefaultConnection` connection string in `appsettings.json`. By default it points to a LocalDB database named `OnlineStoreDb`.

3. Create the database from the migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the app:

   ```bash
   dotnet run
   ```

5. Open the URL shown in the console and go to **Products**.

## Project Structure

```
OnlineStoreMVC/
├── Controllers/        # HomeController, ProductsController (CRUD)
├── Models/             # Product (entity), ProductDto (form model)
├── Services/           # ApplicationDbContex (EF Core DbContext)
├── Migrations/         # EF Core migrations
├── Views/              # Razor views (Home, Products, Shared)
├── wwwroot/            # Static files, Bootstrap/jQuery, product images
└── Program.cs          # App startup and DI setup
```

## Future Improvements

- Payment gateway integration
- User reviews
