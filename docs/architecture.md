# Reading Tracker - Architecture

## Technology
- .NET 10
- ASP.NET Core Blazor Web App
- Interactive Server
- ASP.NET Core Identity
- Entity Framework Core
- SQLite for local development and the one-instance Railway deployment (persistent volume at /data)

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
- Project setup, authentication and security
- Models, services and database
- Deployment

David:
- Navigation and layout
- Book list and forms
- Frontend integration

Wadzanai:
- Reading status and filter components
- Responsive design
- Documentation and testing

## Rules
- Frontend uses the shared service interface.
- Frontend does not access the database directly.
- Book ownership is checked on the server.
- Developers work in separate Git branches.
- Changes are merged using pull requests.
- Final app and demo video ready by end of W06.

## Infrastructure verification

- Railway was selected for .NET 10 Docker hosting.
- Local Docker persistence checks passed.
- The Railway app started in Production and recognized the existing SQLite
  migration history after a Redeploy using the persistent /data volume.
- Full real-user book workflow, public-registration email sender, HTTPS proxy
  configuration, and encryption of stored login keys remain follow-up work.

## Open Decisions
- Final team review of architecture, page wireframes and visual style.
- Real production email provider and secret/key protection strategy.
- Full browser workflow testing and public-domain readiness.

See [deployment.md](deployment.md) and [ui-style-guide.md](ui-style-guide.md).
