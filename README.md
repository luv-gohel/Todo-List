# Todo Application - Project Workflow

## Project Overview

A full-stack Todo application built with **Angular 19** (Frontend) and **.NET Core** (Backend API).

---

## Architecture Diagram

```
┌─────────────────────────────────────────────────────────────────────┐
│                         FRONTEND (Angular)                          │
│                         Todo.Core Project                           │
├─────────────────────────────────────────────────────────────────────┤
│  ┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐      │
│  │  Login   │    │ Register │    │Dashboard │    │  Header  │      │
│  │Component │    │Component │    │Component │    │Component │      │
│  └────┬─────┘    └────┬─────┘    └────┬─────┘    └────┬─────┘      │
│       │               │               │               │             │
│       └───────────────┴───────┬───────┴───────────────┘             │
│                               │                                      │
│                    ┌──────────▼──────────┐                          │
│                    │      Services       │                          │
│                    │  (Auth, Task)       │                          │
│                    └──────────┬──────────┘                          │
│                               │                                      │
│                    ┌──────────▼──────────┐                          │
│                    │    HttpClient       │                          │
│                    │   (API Calls)       │                          │
│                    └──────────┬──────────┘                          │
└───────────────────────────────┼──────────────────────────────────────┘
                                │
                          HTTP Requests
                                │
┌───────────────────────────────▼──────────────────────────────────────┐
│                         BACKEND (.NET Core)                          │
│                         Todo.API Project                             │
├──────────────────────────────────────────────────────────────────────┤
│  ┌────────────────┐              ┌────────────────┐                  │
│  │ AuthController │              │ TaskController │                  │
│  │  /api/Auth/*   │              │  /api/Task/*   │                  │
│  └───────┬────────┘              └───────┬────────┘                  │
│          │                               │                           │
│          └───────────────┬───────────────┘                           │
│                          │                                           │
│               ┌──────────▼──────────┐                                │
│               │   BAL (Business     │                                │
│               │   Access Layer)     │                                │
│               └──────────┬──────────┘                                │
│                          │                                           │
│               ┌──────────▼──────────┐                                │
│               │   DAL (Data         │                                │
│               │   Access Layer)     │                                │
│               └──────────┬──────────┘                                │
└──────────────────────────┼───────────────────────────────────────────┘
                           │
                    ┌──────▼──────┐
                    │  Database   │
                    │ (SQL Server)│
                    └─────────────┘
```

---

## Project Structure

### Frontend (Todo.Core)

```
Todo.Core/src/app/
├── core/
│   ├── guards/
│   │   └── auth-guard.ts        # Protects routes from unauthorized access
│   ├── interceptor/
│   │   └── auth-interceptor.ts  # Adds auth headers to HTTP requests
│   └── services/
│       ├── auth.ts              # Authentication service (login, logout)
│       └── task.ts              # Task CRUD operations service
├── features/
│   ├── auth/
│   │   ├── login/               # Login component
│   │   └── register/            # Register component
│   ├── dashboard/               # Main task management component
│   └── header/                  # Navigation header component
├── shared/
│   └── models/
│       ├── auth.model.ts        # Auth related interfaces
│       └── taskmodel.ts         # Task related interfaces
├── environments/
│   └── environment.ts           # API URL configuration
├── app.routes.ts                # Route definitions
├── app.config.ts                # App configuration
├── app.ts                       # Root component
└── app.html                     # Root template
```

### Backend (.NET)

```
Todo.API/
├── Controllers/
│   ├── AuthController.cs        # Login & Register endpoints
│   └── TaskController.cs        # Task CRUD endpoints
└── Program.cs                   # App configuration & DI

Todo.BAL/
├── Interface/
│   ├── IUserInterface.cs        # Auth business logic contract
│   └── IManageNoteInterface.cs  # Task business logic contract
└── Repository/
    ├── UserRepository.cs        # Auth business logic implementation
    └── ManageNoteRepository.cs  # Task business logic implementation

Todo.DAL/
├── Interface/
│   ├── IUserInterface.cs        # Auth data access contract
│   └── IManageNoteInterface.cs  # Task data access contract
└── Repository/
    ├── UserRepository.cs        # Auth database operations
    └── ManageNoteRepository.cs  # Task database operations

Todo.Models/
├── DTO/
│   └── ApiResponse.cs           # Standard API response model
└── RequestDTO/
    ├── LoginRequest.cs          # Login request model
    ├── RegisterRequest.cs       # Register request model
    └── ListRequest.cs           # Task request model
```

---

## Application Flow

### 1. Authentication Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                      LOGIN FLOW                                  │
└─────────────────────────────────────────────────────────────────┘

User enters credentials
        │
        ▼
┌───────────────────┐
│   Login Component │
│   (login.ts)      │
└────────┬──────────┘
         │ this.authservice.login(credentials)
         ▼
┌───────────────────┐
│   Auth Service    │
│   (auth.ts)       │
└────────┬──────────┘
         │ POST /api/Auth/Login
         ▼
┌───────────────────┐
│  AuthController   │
│  (.NET Backend)   │
└────────┬──────────┘
         │ Validates credentials
         ▼
┌───────────────────┐
│   Returns:        │
│   - statuscode    │
│   - userid (GUID) │
│   - message       │
└────────┬──────────┘
         │
         ▼
┌───────────────────┐
│ Auth Service      │
│ setUserGuid()     │──────► localStorage.setItem('userGuid', guid)
└────────┬──────────┘
         │
         ▼
┌───────────────────┐
│ Navigate to       │
│ /dashboard        │
└───────────────────┘
```

### 2. Logout Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                      LOGOUT FLOW                                 │
└─────────────────────────────────────────────────────────────────┘

User clicks Logout
        │
        ▼
┌───────────────────┐
│  Header Component │
│  logout()         │
└────────┬──────────┘
         │
         ▼
┌───────────────────┐
│   Auth Service    │
│   logout()        │──────► localStorage.removeItem('userGuid')
└────────┬──────────┘
         │
         ▼
┌───────────────────┐
│ Navigate to       │
│ /login            │
└───────────────────┘
```

### 3. Route Protection (Auth Guard)

```
┌─────────────────────────────────────────────────────────────────┐
│                    ROUTE GUARD FLOW                              │
└─────────────────────────────────────────────────────────────────┘

User tries to access /dashboard
        │
        ▼
┌───────────────────┐
│   Auth Guard      │
│ (auth-guard.ts)   │
└────────┬──────────┘
         │ authService.isLoggedIn()
         ▼
    ┌────┴────┐
    │         │
  TRUE      FALSE
    │         │
    ▼         ▼
┌───────┐  ┌────────────┐
│ Allow │  │ Redirect   │
│ Access│  │ to /login  │
└───────┘  └────────────┘
```

### 4. Task CRUD Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    TASK OPERATIONS                               │
└─────────────────────────────────────────────────────────────────┘

Dashboard Component
        │
        ├──── loadTasks() ────► GET  /api/Task/GetNotes?UserGID=xxx
        │
        ├──── addTask() ──────► POST /api/Task/AddNote
        │
        ├──── updateTask() ───► POST /api/Task/UpdateNote
        │
        └──── deleteTask() ───► DELETE /api/Task/DeleteNote
```

---

## API Endpoints

### Auth Controller (`/api/Auth`)

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| POST | `/Login` | User login | `{ email, password }` |
| POST | `/Register` | User registration | `{ name, email, password }` |

### Task Controller (`/api/Task`)

| Method | Endpoint | Description | Request Body/Params |
|--------|----------|-------------|---------------------|
| GET | `/GetNotes` | Get all tasks | `?UserGID=xxx` |
| POST | `/AddNote` | Create new task | `{ UserGID, TaskTitle, TaskDescription, Priority }` |
| POST | `/UpdateNote` | Update task | `{ TaskGID, UserGID, TaskTitle, TaskDescription, Priority }` |
| DELETE | `/DeleteNote` | Delete task | `{ TaskGID, UserGID, IsDeleted: true }` |

---

## Data Models

### Task Model (Frontend)

```typescript
interface Taskmodel {
    ID?: number;
    TaskGID?: string;
    UserGID: string;
    TaskTitle: string;
    TaskDescription: string;
    Priority: number;        // 1=Low, 2=Medium, 3=High
    IsDeleted?: boolean;
    DeletedDate?: Date;
    UpdatedDate?: Date;
    CreatedDate?: Date;
}
```

### API Response

```typescript
interface ApiResponse {
    statuscode: number;      // 101=Success, 102=Updated, 100=Error
    message: string;
    responselist?: any[];    // List of items (for GET requests)
    userid?: string;         // User GUID (for login)
}
```

---

## Component Responsibilities

| Component | File | Responsibilities |
|-----------|------|------------------|
| **Login** | `login/login.ts` | User authentication, form validation, redirect to dashboard |
| **Register** | `register/register.ts` | New user registration, form validation |
| **Dashboard** | `dashboard/dashboard.ts` | Task CRUD operations, display task list |
| **Header** | `header/header.ts` | Navigation, show/hide links based on auth state, logout |

---

## Services

| Service | File | Methods |
|---------|------|---------|
| **Auth** | `auth.ts` | `login()`, `register()`, `logout()`, `isLoggedIn()`, `getUserGuid()`, `setUserGuid()` |
| **Task** | `task.ts` | `addTask()`, `updateTask()`, `deleteTask()`, `getAllTask()` |

---

## Guards & Interceptors

| Name | File | Purpose |
|------|------|---------|
| **authGuard** | `auth-guard.ts` | Protects routes, redirects to login if not authenticated |
| **authInterceptor** | `auth-interceptor.ts` | Adds authentication headers to HTTP requests |

---

## Status Codes

| Code | Meaning |
|------|---------|
| 100 | Error / Failed |
| 101 | Success (Add/Delete) |
| 102 | Success (Update) |

---

## Local Storage

| Key | Value | Purpose |
|-----|-------|---------|
| `userGuid` | User's GUID string | Identifies logged-in user |

---

## Environment Configuration

```typescript
// environment.ts
export const environment = {
    production: false,
    apiUrl: 'https://localhost:7185/api/'
}
```

---

## Database Table Structure

### tbl_Users

| Column | Type | Description |
|--------|------|-------------|
| ID | int | Primary Key |
| UserGID | uniqueidentifier | User GUID |
| Name | varchar | User name |
| Email | varchar | User email |
| Password | varchar | User password |

### tbl_List

| Column | Type | Description |
|--------|------|-------------|
| ID | int | Primary Key |
| TaskGID | uniqueidentifier | Task GUID |
| UserGID | uniqueidentifier | Foreign Key to User |
| TaskTitle | varchar | Task title |
| TaskDescription | varchar | Task description |
| Priority | int | 1=Low, 2=Medium, 3=High |
| IsDeleted | bit | Soft delete flag |
| DeletedDate | datetime | When deleted |
| UpdatedDate | datetime | Last update time |
| CreatedDate | datetime | Creation time |

---

## How to Run

### Backend (.NET)

```bash
cd Todo.API
dotnet run
```
API runs at: `https://localhost:7185`

### Frontend (Angular)

```bash
cd Todo.Core
ng serve
```
App runs at: `http://localhost:4200`

---

## User Flow Summary

```
1. User opens app → Sees Login/Register in header
2. User registers → Account created → Redirected to login
3. User logs in → UserGUID saved → Redirected to dashboard
4. Dashboard loads → Fetches user's tasks → Displays in table
5. User can Add/Edit/Delete tasks
6. User clicks Logout → UserGUID cleared → Redirected to login
7. If user tries to access /dashboard without login → Auth Guard redirects to /login
```
