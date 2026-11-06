# Reading Tracker - Architecture

## Technology
- .NET 10
- ASP.NET Core Blazor Web App
- Interactive Server
- ASP.NET Core Identity
- Entity Framework Core
- SQLite for local development

## Main Features
- User registration and login
- Personal book list
- Add, edit, and delete books
- Reading status
- Filter books by status

## Book Model
- Id: int
- Title: string (required)
- Author: string (required)
- Status: Want to Read, Reading, Finished
- UserId: string (book owner)

## Application Structure
- Components/Pages/Books: Book pages and forms
- Components/Layout: Navigation and layout
- Models: Book and related data types
- Services: BookService and its interface
- Data: EF Core database configuration

## Responsibilities
Steven:
- Project setup
- Authentication
- Models, Services and Database
- Security and Deployment

Wadzanai:
- Pages and Forms
- Navigation and Layout
- Reading Status and Filters
- Responsive Design

## Rules
- Frontend uses the shared service interface.
- Frontend does not access the database directly.
- Book ownership is checked on the server.
- Developers work in separate Git branches.
- Changes are merged using pull requests.
- Final app and demo video ready by end of W06.

## Open Decisions
- Production hosting and persistent database
- Final review of architecture with both members
