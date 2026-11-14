# Reading Tracker - Deployment

## Technology

- .NET 10
- ASP.NET Core Blazor
- SQLite
- Docker
- Railway

## Docker

Build from the repository root:

docker build -t readingtracker:railway .

The container listens on PORT, default 8080.

## Railway Settings

Connect the GitHub repository and use the root Dockerfile.

Create a persistent volume mounted at:

/data

Set the service variable:

PersistentDataPath=/data

The app stores:

- SQLite database: /data/app.db
- Login protection keys: /data/keys

Keep one app instance when using SQLite.

## Database

The app applies EF Core migrations when starting
outside the Development environment.

Use the existing EF Core migrations.

Review and back up data before future schema changes.

## Security

Do not commit passwords, database files or secrets.

Use HTTPS through the hosting provider.

Review trusted proxy and forwarded-header settings
before making the service public.

The starter currently uses a no-op email sender.
Real email confirmation is not ready for public use.

Data Protection keys are persisted on the volume.
File-based key storage is not encrypted at rest
by this configuration.

## Verification Before Completion

- Docker image builds.
- Container starts.
- Database is created on the persistent volume.
- Registration and login work.
- Data remains after a restart.
- The deployed website is accessible.
- Production secrets and email handling are reviewed.

Do not mark hosting as fully complete until
the live deployment and persistence test pass.
