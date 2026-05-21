# Programme Management System API

## Project Description
This project provides a RESTful API for a Programme Management System, allowing for the management of students, lecturers, modules, registrations, and module assignments. It is built using ASP.NET Core 8.0 and Entity Framework Core for data persistence.

## Features
- **Student Management**: Create, read, update, and delete student records. Includes student registration details.
- **Lecturer Management**: Create, read, update, and delete lecturer records. Includes assigned module details.
- **Module Management**: Create, read, update, and delete module records. Includes enrolled students and assigned lecturers.
- **Registration Management**: Handle student enrollments in modules, with checks for duplicate registrations.
- **Module Assignment Management**: Handle lecturer assignments to modules, with checks for duplicate assignments.
- **Data Validation**: Ensures data integrity with unique constraints for emails and module codes, and prevents duplicate registrations/assignments.
- **Swagger/OpenAPI**: Automatically generated API documentation for easy testing and understanding of endpoints.

## Technology Stack
- **Framework**: ASP.NET Core 8.0
- **Language**: C#
- **ORM**: Entity Framework Core
- **Database**: SQL Server (configured for LocalDB by default)
- **API Documentation**: Swagger/OpenAPI

## Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.
- A SQL Server instance (LocalDB is sufficient for local development).

### Installation
1.  **Clone the repository or extract the project archive.**
    ```bash
    unzip ProgrammeManagementSystemApi.zip
    cd ProgrammeManagementSystemApi
    ```

2.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```

3.  **Update Connection String:**
    Open `appsettings.json` and ensure the `DefaultConnection` string points to your SQL Server instance. By default, it's configured for LocalDB:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProgrammeManagementSystemApi;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

### Database Setup (Migrations)
This project uses Entity Framework Core Migrations to manage the database schema. Follow these steps to create and apply migrations:

1.  **Create Initial Migration** (if not already present):
    ```bash
    dotnet ef migrations add InitialCreate
    ```

2.  **Apply Migrations to Database:**
    ```bash
    dotnet ef database update
    ```
    This will create the database and tables based on the defined models.

### Running the Application
To run the API, navigate to the project directory in your terminal and execute:

```bash
dotnet run
```

The API will typically run on `https://localhost:7001` (or a similar port). You can find the exact URL in the console output.

### Accessing API Documentation (Swagger)
Once the application is running, you can access the Swagger UI in your web browser at:

`https://localhost:{port}/swagger`

Replace `{port}` with the actual port number the application is running on.

## API Endpoints

### Students
- `GET /api/Students`: Retrieve all students.
- `GET /api/Students/{id}`: Retrieve a specific student by ID, including their registrations and modules.
- `POST /api/Students`: Create a new student.
- `PUT /api/Students/{id}`: Update an existing student.
- `DELETE /api/Students/{id}`: Delete a student.

### Lecturers
- `GET /api/Lecturers`: Retrieve all lecturers.
- `GET /api/Lecturers/{id}`: Retrieve a specific lecturer by ID, including their module assignments and modules.
- `POST /api/Lecturers`: Create a new lecturer.
- `PUT /api/Lecturers/{id}`: Update an existing lecturer.
- `DELETE /api/Lecturers/{id}`: Delete a lecturer.

### Modules
- `GET /api/Modules`: Retrieve all modules.
- `GET /api/Modules/{id}`: Retrieve a specific module by ID, including its registrations, students, and assigned lecturers.
- `POST /api/Modules`: Create a new module.
- `PUT /api/Modules/{id}`: Update an existing module.
- `DELETE /api/Modules/{id}`: Delete a module.

### Registrations
- `GET /api/Registrations`: Retrieve all student registrations.
- `GET /api/Registrations/{id}`: Retrieve a specific registration by ID, including student and module details.
- `POST /api/Registrations`: Create a new student registration for a module.
- `DELETE /api/Registrations/{id}`: Delete a student registration.

### ModuleAssignments
- `GET /api/ModuleAssignments`: Retrieve all module assignments.
- `GET /api/ModuleAssignments/{id}`: Retrieve a specific module assignment by ID, including lecturer and module details.
- `POST /api/ModuleAssignments`: Create a new lecturer assignment for a module.
- `DELETE /api/ModuleAssignments/{id}`: Delete a module assignment.

## Models

### Student
- `StudentID` (int): Primary Key
- `FirstName` (string)
- `LastName` (string)
- `Email` (string, unique)
- `PhoneNumber` (string, optional)
- `YearOfStudy` (int, range 1-4)
- `Registrations` (Collection of `Registration`)

### Lecturer
- `LecturerID` (int): Primary Key
- `FirstName` (string)
- `LastName` (string)
- `Email` (string, unique)
- `Department` (string)
- `ModuleAssignments` (Collection of `ModuleAssignment`)

### Module
- `ModuleID` (int): Primary Key
- `ModuleName` (string)
- `ModuleCode` (string, unique)
- `Credits` (int, range 1-30)
- `AcademicYear` (string)
- `Registrations` (Collection of `Registration`)
- `ModuleAssignments` (Collection of `ModuleAssignment`)

### Registration
- `RegistrationID` (int): Primary Key
- `StudentID` (int): Foreign Key to `Student`
- `ModuleID` (int): Foreign Key to `Module`
- `DateRegistered` (DateTime)
- `Student` (Navigation property)
- `Module` (Navigation property)

### ModuleAssignment
- `AssignmentID` (int): Primary Key
- `LecturerID` (int): Foreign Key to `Lecturer`
- `ModuleID` (int): Foreign Key to `Module`
- `Lecturer` (Navigation property)
- `Module` (Navigation property)

