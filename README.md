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

## Project Status (November 14, 2026)

Implemented and merged into `main`:
- .NET 10 Blazor starter with ASP.NET Core Identity (local registration, email
  confirmation, login and logout verified).
- Book model, ReadingStatus enum and IBookService interface.
- EF Core SQLite Books migration and persistent BookService with server-side
  owner checks (15 isolated CRUD/security tests passed).
- Development-only MockBookService with sample books.
- Docker image and Railway deployment: production startup succeeded; SQLite
  migration history persisted across a Railway redeploy.
- Architecture, API, deployment and proposed UI planning documentation.

Still being developed or reviewed:
- David's navigation, My Books page and Add/Edit/Delete forms.
- Wadzanai's status/filter components and responsive checks.
- UI wireframe/team review and full end-to-end browser tests with two accounts.
- Production email sender, trusted HTTPS proxy settings and Data Protection
  keyring encryption before public registration.
- Final cloud app with the book workflow and demo.

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

Note: These setup steps still need to be checked on a fresh clone. Development uses the real BookService by default. Set `UseMockBookService=true` to use temporary sample books.

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

## GitHub Workflow and File Ownership

1. Run `git switch main && git pull --ff-only origin main`.
2. Start a branch such as `feature/david-books-page` or
   `feature/wadzanai-status-filter`.
3. Edit only the files in your responsibility area; coordinate shared changes.
4. Run `dotnet build ReadingTracker/ReadingTracker.csproj` from the repo root.
5. Commit, push, open a pull request targeting `main`, and review before merge.
6. After merge, teammates pull `main` again. Avoid direct commits to `main`.

**Ownership:** Steven owns `Models/`, `Services/`, `Data/`, Identity
integration, `Program.cs` and deployment. David owns book pages, forms and
main navigation/layout. Wadzanai owns standalone status/filter components,
design guidance and responsive testing. Shared interfaces must be discussed
before changing them. Do not edit a teammate's Razor files without agreement.

**Still to confirm for RT-04:** David and Wadzanai must verify repository
access, their own feature branches and agreement with these rules.

Never commit SQLite DBs, passwords, tokens, API keys or secret files.

## Project Documentation

- [Architecture](docs/architecture.md)
- [Book interface contract](docs/book-contract.md)
- [Frontend development guide](docs/frontend-start.md)
- [Page wireframes (for David's review)](docs/ui-planning.md)
- [UI style guide (team baseline)](docs/ui-style-guide.md)
- [Railway deployment and security notes](docs/deployment.md)

## Project Goal

We plan to finish development, testing, and the final demo video by the end of Week 6.

Week 7 will be used as extra time if needed.
