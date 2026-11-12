# Reading Tracker

CSE 325 – Group Project
BYU-Idaho – .NET Software Development

## About the Project

Reading Tracker is a web application that helps users organize their books.

Users will be able to create an account, add books, change their reading status, and manage their personal reading list.

## Team Members

- Steven Kemendics
- David Esumei
- Wadzanai Murombedzi

## Technology

- C# and .NET 10
- ASP.NET Core Blazor Web App
- Interactive Server
- ASP.NET Core Identity
- Entity Framework Core
- SQLite
- GitHub and Trello

## Project Status

The Blazor starter project is available on GitHub.

Already prepared:
- Blazor application
- User registration and login
- Book model and reading status
- IBookService interface
- Architecture documentation

Still being developed:
- Book database and BookService
- Book list and forms
- Reading status and filters
- Testing and deployment

## How to Run the Project

### Requirements

Install the .NET 10 SDK.

### Step 1: Clone the Repository

```bash
git clone https://github.com/STU6266/CSE325-Group-Project.git
cd CSE325-Group-Project/ReadingTracker
```

### Step 2: Restore Packages

```bash
dotnet restore
```

### Step 3: Prepare the Database

The application uses a local SQLite database.

Install the EF Core command-line tool if needed:

```bash
dotnet tool install --global dotnet-ef --version 10.0.13
```

If the tool is already installed, skip this command.

Create the local database:

```bash
dotnet ef database update
```

### Step 4: Start the Application

```bash
dotnet run --launch-profile http
```

Open this address in your browser:

http://localhost:5261

You can create an account and log in.

Note: These setup steps still need to be checked on a fresh clone.

## Team Responsibilities

### Steven – Backend
- Authentication
- Book model and services
- Database
- Security
- Deployment

### David – Frontend
- Navigation and layout
- Book list
- Add, edit, and delete forms
- Frontend integration

### Wadzanai – Components and Design
- Reading status components
- Status filters
- Responsive design
- Documentation and testing

## GitHub Workflow

1. Update your local main branch.
2. Create your own feature branch.
3. Work on your assigned files.
4. Commit your changes.
5. Push your branch to GitHub.
6. Create a pull request to main.

Please do not push directly to main.

Do not commit local databases, passwords, or secret files.

## Project Documentation

- docs/architecture.md
- docs/book-contract.md

## Project Goal

We plan to finish development, testing, and the final demo video by the end of Week 6.

Week 7 will be used as extra time if needed.
