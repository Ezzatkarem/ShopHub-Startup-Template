# MyShop

MyShop is a full-featured E-Commerce web application built with ASP.NET Core MVC using the N-Tier Architecture. The project provides a complete shopping experience for customers and a secure administration dashboard for managing products, categories, and users.

The application was developed to apply software engineering principles such as Separation of Concerns, Repository Pattern, Unit of Work, Dependency Injection, and Role-Based Authorization.

---

# Features

## Customer

* Register a new account
* Email confirmation
* Secure login and logout
* Browse all products
* Browse product categories
* View product details

## Administrator

* Dashboard
* Product Management

  * Create
  * Edit
  * Delete
* Category Management

  * Create
  * Edit
  * Delete
* User Management

  * View users
  * Change user roles
  * Lock accounts
  * Unlock accounts
  * Delete users

---

# Technologies

## Backend

* ASP.NET Core MVC
* C#
* Entity Framework Core
* ASP.NET Core Identity
* AutoMapper
* LINQ

## Database

* SQL Server

## Frontend

* HTML5
* CSS3
* Bootstrap 5
* JavaScript
* jQuery
* AJAX
* DataTables
* AdminLTE

---

# Architecture

The application follows the N-Tier Architecture.

```
Presentation Layer
        │
Business Logic Layer
        │
Data Access Layer
        │
SQL Server Database
```

The project is organized into the following layers.

```
myshop.Web
myshop.BLL
myshop.DAL
myshop.Entities
```

---

# Authentication and Authorization

Authentication is implemented using ASP.NET Core Identity.

The application includes:

* User Registration
* Secure Login
* Email Confirmation
* Password Hashing
* Role Management
* Authorization Policies

Roles

* Admin
* Customer

---

# Design Patterns

* N-Tier Architecture
* Repository Pattern
* Generic Repository
* Unit of Work
* Service Layer
* Dependency Injection

---

# Security

* ASP.NET Core Identity
* Authorization Policies
* Role-Based Access Control
* Email Verification
* Anti-Forgery Protection
* Password Hashing

---

# Packages

* Entity Framework Core
* ASP.NET Core Identity
* AutoMapper
* SQL Server Provider

---

# Installation

Clone the repository.

```bash
git clone https://github.com/your-username/MyShop.git
```

Navigate to the project.

```bash
cd MyShop
```

Update the connection string inside:

```
appsettings.json
```

Apply migrations.

```bash
Update-Database
```

or

```bash
dotnet ef database update
```

Run the application.

---

# Screenshots

Add screenshots for:

* Home Page
* Product List
* Product Details
* Category Management
* Product Management
* User Management
* Admin Dashboard

---

# Future Improvements

* Shopping Cart
* Checkout
* Orders
* Online Payments
* Wishlist
* Product Reviews
* Product Search
* Dashboard Analytics
* REST API
* JWT Authentication

---

# Learning Outcomes

This project demonstrates practical experience with:

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* Authentication
* Authorization
* AutoMapper
* Repository Pattern
* Generic Repository
* Unit of Work
* Dependency Injection
* CRUD Operations
* File Upload
* Role Management

---

# Author

Ezzat Karem Medhat

Computer Science Student

Full Stack .NET Developer

---

# License

This project was developed for educational and portfolio purposes.
