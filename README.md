# FlatManage — System Architecture & Implementation

The FlatManage system has been built using a **Clean Architecture** approach with .NET 10, ensuring scalability, maintainability, and enterprise-grade security.

## 🏗️ Project Structure
- **FlatManage.Domain**: Core entities (Building, Unit, Tenant, etc.), Enums, and Repository Interfaces.
- **FlatManage.Application**: Business logic (MonthlyInvoiceService, OccupancyService), DTOs, and Service Interfaces.
- **FlatManage.Infrastructure**: Data access (EF Core, SQL Server), Identity configuration, Repository implementations, and External Services (SMS/Email).
- **FlatManage.API**: RESTful endpoints, JWT Auth, SignalR Hub, and Hangfire background jobs.
- **FlatManage.Web**: MVC Web Portal with a premium, responsive dashboard for Admin and Tenants.

## 🚀 Key Features Implemented
- **Full Entity Grid**: 22 normalized database entities with proper relationships.
- **Soft Delete & Audit Log**: Automatically handles record deletion and tracks every change.
- **Monthly Billing**: Service to auto-generate invoices and calculate late fees.
- **Real-time Dashboard**: SignalR integration for instant updates on payments and occupancy.
- **Premium UI**: Modern Bootstrap 5 layout with Chart.js visualization.
- **Automated Seeding**: Ready-to-use demo data (Buildings, Floors, Units, Tenants).

## 🛠️ How to Run
1. Ensure **SQL Server** and **Redis** are running.
2. Update connection strings in [FlatManage.API/appsettings.json](file:///d:/Yeamin/Flat-Management/FlatManage.API/appsettings.json).
3. Run migrations or let the seeder create the DB:
   ```powershell
   dotnet run --project FlatManage.API
   ```
4. Start the Web application:
   ```powershell
   dotnet run --project FlatManage.Web
   ```

## 🔒 Default Credentials
- **Admin**: `admin@flatmanage.com` / `Admin@123`
- **Tenant**: `tenant1@example.com` / `Tenant@123`
