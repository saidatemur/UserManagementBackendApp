# User Management Web Application

This project is a **full-stack user management system** built using:

- **Frontend:** React + TypeScript + Bootstrap
- **Backend:** ASP.NET Core Web API
- **Database:** MS SQL Server

The application supports **user registration, login, authentication**, and a protected **admin panel** with user status management capabilities (block, unblock, delete).

## Features

✅ User registration and login  
✅ JWT-based authentication  
✅ Unique index on email in SQL Server  
✅ User role-based access (only authenticated & non-blocked users can access admin panel)  
✅ Professional-looking UI using **Bootstrap**  
✅ Admin panel with:
- Select All / Deselect All via checkboxes
- Sorted table by **last login time**
- Toolbar with `Block`, `Unblock`, `Delete` actions
- Status messages (e.g., success/failure)
- Tooltips for better UX  
✅ Responsive layout (mobile/desktop)  
✅ Proper error handling and redirection for blocked/deleted users

## Technologies Used

| Tech Stack         | Description                             |
|--------------------|-----------------------------------------|
| ASP.NET Core API   | Backend with secure endpoints           |
| React + TypeScript | Frontend SPA                            |
| Bootstrap          | CSS Framework for UI                    |
| SQL Server         | Relational database                     |
| Entity Framework   | ORM for DB communication                |
| JWT                | Token-based authentication              |

## Database Configuration

- **Email Uniqueness:** Enforced with a **unique index** in SQL Server.
- Actual SQL Table Schema:
  ```sql
  CREATE TABLE Users (
      Id INT PRIMARY KEY IDENTITY,
      Name NVARCHAR(100),
      Email NVARCHAR(100) UNIQUE,
      PasswordHash NVARCHAR(MAX),
      IsBlocked BIT DEFAULT 0,
      LastLogin DATETIME,
      RegistrationDate DATETIME DEFAULT GETDATE(),
      ResetToken NVARCHAR(255),
      ResetTokenExpiry DATETIME
  );

  CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
  ```

## How to Run

### Backend (ASP.NET Core)

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

- Ensure the connection string is configured in `appsettings.json`.

### Frontend (React)

```bash
cd frontend
npm install
npm run dev
```

- The frontend should run on [http://localhost:5174](http://localhost:5174)

## Admin Panel Access

After login, the user is redirected to the admin panel:

- **Table View:** Users are listed with name, email, last login, and status.
- **Toolbar:** Use the toolbar buttons to manage selected users:
  - 🟥 `Block` (Text button)
  - ✅ `Unblock` (Icon button)
  - 🗑 `Delete` (Icon button)
- Table is **sorted by Last Login Time**.

## Deployment

The application can be deployed on:
- Azure (App Service + SQL Database)
- Railway / Vercel (Frontend)
- Any VM or Docker-based host

## Notes

- Blocked users cannot log in.
- Deleted users can re-register.
- Only authenticated & non-blocked users can access the admin panel.
- All server-side checks (auth + block status) are enforced on **every protected request**.

## License

This project is for educational purposes.

---

**Created by:** Saida Temurpulatova