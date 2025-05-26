# 🗒️ Note App .NET Web API

A lightweight and efficient Note-Taking REST API built with **ASP.NET Core** and **Dapper**. This project allows you to create, view, edit, and delete notes.

---

## 🧱 Tech Stack

* .NET 8
* ASP.NET Core Web API
* Dapper ORM
* SQL Server
* Host on AWS Elastic Beanstalk

---


## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/note-api.git
cd note-api
```

### 2. Database
Create a database then run this sql staement to create the Notes table:

```sql
CREATE TABLE Notes (
    Id INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL
);
```

### 3. Configure Connection String
Update appsettings.json:
```c#
"ConnectionStrings": {
    "DefaultConnection": "[YOUR_CONNECTION_STRING]"
}
```

### 3. Run the API

```bash
cd NoteApi
dotnet build
dotnet run
```