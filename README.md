# WBL – ASP.NET Core MVC Web Application

## Overview

WBL is a web application developed using ASP.NET Core MVC and C#. The project demonstrates the development of a database-driven web application using a structured Model-View-Controller (MVC) architecture.

It incorporates SQL Server database integration, Entity Framework Core for data access, and ASP.NET Core Identity for user authentication and account management.

## Technology Stack

- **Backend:** C#, ASP.NET Core MVC
- **Frontend:** Razor Views, ASP.NET Core web interface
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Development Environment:** Visual Studio / .NET

## Technical Implementation

- **MVC Architecture:** Organises application logic into models, views, and controllers to support maintainability and separation of concerns.
- **Database Integration:** Uses Entity Framework Core with SQL Server to manage application data.
- **Authentication:** Configures ASP.NET Core Identity with custom application users and role support.
- **Database Migrations:** Includes Entity Framework Core migrations for managing database schema changes.
- **Application Routing:** Uses controller-based routing alongside Razor Pages for Identity functionality.
- **Logging:** Configures console logging to support application development and troubleshooting.

## Project Structure

- `Controllers/` – Application request handling and controller logic
- `Models/` – Application data models
- `Views/` – Razor-based user interface
- `Data/` – Database context and data configuration
- `Migrations/` – Entity Framework Core database migrations
- `Areas/Identity/` – Identity-related Razor Pages
- `Utilities/` – Supporting application utilities
- `wwwroot/` – Static web assets
- `Program.cs` – Application startup and service configuration

## Project Purpose

This project demonstrates practical experience in building an ASP.NET Core MVC application, configuring database connectivity, integrating user authentication, and organising a web application using established software engineering practices.

## Author

**Umar Norhisham**

GitHub: [umarhaziq](https://github.com/umarhaziq)
