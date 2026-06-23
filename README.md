# 🎓 Student Event Management API

A complete backend RESTful API for managing student events, registrations, and feedback — built with **ASP.NET Core 10** and **Entity Framework Core 10**.

---

## 🛠️ Tech Stacks

| Technology | Details |
|-----------|---------|
| Framework | ASP.NET Core 10 |
| ORM | Entity Framework Core 10 |
| Database | SQL Server (SSMS) |
| Documentation | Swagger / OpenAPI 3.0 |
| Language | C# |

---

## 📁 Project Structure

```
StudentEventManagement/
├── Controllers/      → API Endpoints
├── DTOs/             → Data Transfer Objects
├── Data/             → DbContext
├── Migrations/       → EF Core Migrations
├── Models/           → Database Entities
├── Services/         → Business Logic
└── Program.cs        → App Entry Point
```

---

## 🚀 API Endpoints

### Events
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/events | List all upcoming events |
| GET | /api/events/{id} | Get event by ID |
| POST | /api/events | Create a new event |
| PUT | /api/events/{id} | Update an event |
| DELETE | /api/events/{id} | Delete an event |
| GET | /api/events/search?query=xyz | Search by name or venue |
| GET | /api/events/filter?sort=asc | Filter by date |

### Students
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/students | List all students |
| POST | /api/students | Register a new student |

### Registrations
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/registrations | Register student for event |
| GET | /api/registrations/event/{eventId} | Get registrations by event |

### Feedback
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/feedback | Submit feedback for event |
| GET | /api/feedback/event/{eventId} | Get feedback by event |

---

## ⚙️ Setup Instructions

### Prerequisites
- .NET 10 SDK
- SQL Server + SSMS
- VS Code with C# Dev Kit

### Steps

**1. Clone the repository**
```bash
git clone https://github.com/Talha2503/Student-Event-Management.git
cd Student-Event-Management
```

**2. Update connection string in `appsettings.json`**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=StudentEventDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**3. Apply database migrations**
```bash
dotnet ef database update
```

**4. Run the project**
```bash
dotnet run
```

**5. Open Swagger UI**
```
http://localhost:5126
```

---

## ✅ Features

- Full CRUD for Events
- Student registration with duplicate prevention
- Feedback only allowed after event date
- Search and filter events
- Proper HTTP status codes and error handling
- Swagger API documentation
- Clean layered architecture

---

## 👨‍💻 Author

**Muhammad Talha** — BS Software Engineering, Iqra University Karachi
```
