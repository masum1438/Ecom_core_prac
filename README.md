# 🛒 **Ecom_core_prac**

A lightweight, modular **E-commerce Practice Project** built using **ASP.NET Core MVC**, **ADO.NET**, **SQL Server**, and integrated with the external **[DummyJSON Products API](https://dummyjson.com/)**. This project showcases a full-stack implementation including **custom authentication**, **dynamic product loading via API**, **client-side cart management**, and **secure checkout**—all without ASP.NET Identity.

---

## 🌐 **Live Demo Features**

### 🔐 User Authentication
- **Custom login, signup, and password reset** implemented using raw SQL queries
- Session-based authentication (no ASP.NET Identity)
- Backend integration with SQL Server `CoreMember` table for secure validation

### 📦 Dynamic Product Integration via API
- Products are **fetched from the public [DummyJSON API](https://dummyjson.com/products)**
- AJAX (`$.get`) is used to dynamically render product lists and details on the client side
- Cart and product details pages use real-time API calls

### 🛒 Cart & Checkout System
- **Cart state stored in `localStorage`** for persistent user experience
- Live cart count and total price calculation
- Quantity auto-increment on repeated adds
- Checkout page handles shipping, phone, and payment inputs via form

### 🧾 Order & Customer Management
- Customer order details are saved in the database via stored procedures
- Checkout form posts data to the server via AJAX for smooth UX
- Admin can view order history in a responsive dashboard

---

## 🔧 **Tech Stack**

| Layer         | Technology                       |
|---------------|----------------------------------|
| Frontend      | HTML, Bootstrap, jQuery, AJAX    |
| Backend       | ASP.NET Core MVC (.NET 6)        |
| Data Access   | ADO.NET (no EF Core)             |
| Database      | SQL Server + Stored Procedures   |
| External API  | [DummyJSON](https://dummyjson.com/products) |

---

## 🗃️ **Core Models**

### ✅ `BaseAccount.cs`
Handles:
- User login, registration, password reset
- Communication with SQL Server stored procedures

### ✅ `BaseProduct.cs`
- Represents product structure (fetched from API)
- Fields: `id`, `title`, `description`, `price`, `images`, etc.

### ✅ `BaseEquipment.cs`
- Used for admin view of customer orders
- Fields: `CustId`, `CustName`, `ProductDetails`, `Address`, `PhoneNumber`, `Price`, etc.

---

## 🔌 API Usage

### 🌍 DummyJSON Product API Integration

Products are **not stored in the local DB**, instead:
- Product lists and details are fetched dynamically using `https://dummyjson.com/products`
- Each item is rendered in the frontend using jQuery & Bootstrap
- On "Add to Cart", data is fetched by `id` using `https://dummyjson.com/products/{id}`

> ✅ This approach keeps the backend clean and simulates integration with a real product API.

---

## 🛠️ Stored Procedures Used

| Procedure             | Description                             |
|-----------------------|-----------------------------------------|
| `spCore_InsMember`    | Register new user in `CoreMember` table |
| `spCore_InsCustomer`  | Save order data with customer info      |
| `spCore_LstCustomer`  | Admin view of all customer orders       |
| `spOst_InsErrorLog`   | Log internal errors                     |

---



