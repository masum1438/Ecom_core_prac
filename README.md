# 🛒 Ecom_core_prac

A lightweight, database-driven **E-commerce Practice Project** built with **ASP.NET Core MVC**, **ADO.NET**, and **SQL Server** using stored procedures. The project demonstrates user authentication, product listing, cart functionality, and customer order management.

---

## 📌 Features

### 🔐 User Authentication
- **Custom Login/Signup** with session-based authentication
- Password Reset functionality
- User validation using raw SQL (no ASP.NET Identity)

### 🧾 Product & Customer Management
- Fetches product/customer data using stored procedures
- Display product dashboard and details
- Add customer order data with payment details

### 🛍️ Cart & Checkout Pages
- Simple cart and checkout views
- Form-based input handling using `IFormCollection`

### ⚙️ Admin Features
- Add new users and customers via stored procedures
- Prevent duplicate usernames
- Password reset functionality with verification

---

## 🧱 Architecture Overview

### 🔧 Backend
- **Framework:** ASP.NET Core MVC (.NET 6 or above)
- **ORM:** ADO.NET with raw SQL and stored procedures
- **Database:** SQL Server

### 🗂️ Models

#### `BaseAccount.cs`
Handles:
- User login and registration
- Password reset
- Save user/customer via stored procedures

#### `BaseEquipment.cs`
Keyless model for displaying customer data:
- Properties: `CustId`, `CustName`, `ProductDetails`, `Address`, `PhoneNumber`, `Price`, etc.

#### `BaseProduct.cs`
Detailed product structure with:
- Product metadata
- Dimensions, tags, reviews, and images

#### `DbConnection.cs`
- Centralized connection string handler
- Error logging via stored procedure

---

## 🧮 Database Interaction

- **Table:** `CoreMember`
- **Stored Procedures:**
  - `spCore_InsMember` — Insert a new member
  - `spCore_InsCustomer` — Insert customer data
  - `spCore_LstCustomer` — List customer/product data
  - `spOst_InsErrorLog` — Log errors

---

## 🖥️ Project Structure

