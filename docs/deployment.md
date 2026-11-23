# Reading Tracker - Deployment and Persistence (RT-06)

## Confirmed setup

- Runtime: ASP.NET Core Blazor, .NET 10, EF Core SQLite.
- Host: Railway, connected to the GitHub `main` branch.
- Build: multi-stage root `Dockerfile`, Linux runtime.
- Service environment: `Production`.
- Railway service variable: `PersistentDataPath=/data`.
- Railway persistent volume mounted at `/data`.
- SQLite database: `/data/app.db`.
- ASP.NET Core Data Protection key files: `/data/keys`.
- One application instance. Do not scale SQLite instances horizontally.

**Verification on 2026-11-14:** Railway deployment logs reported
`Applying migration '20261114110012_AddBooks'` on first start and
`No migrations were applied. The database is already up to date.` after a
Redeploy using the same volume. Both runs reported `Application started`
and listened on port 8080. This demonstrates persistent migration history
across a live Railway redeploy, **not yet a complete browser workflow test
with saved user books**.

Local Docker checks also passed: HTTP 200, database schema, data marker and
Data Protection key files survived a container replacement, and SQLite
`PRAGMA integrity_check` returned `ok`.

## Build and startup

From the repository root:

```bash
docker build -t readingtracker:railway .
```

Railway builds the root Dockerfile and passes its `PORT` at runtime
(default 8080). Production app startup invokes
`ApplicationDbContext.Database.MigrateAsync()` for existing migrations.

In Development, run with `dotnet run --launch-profile http` inside the
`ReadingTracker` directory. To test the real database service locally,
set `UseMockBookService=true` to use optional sample books. Development
and Production both use `BookService` by default.

## Railway deployment steps (for W05)

1. Create a Railway project and connect the GitHub repository.
2. Deploy from `main` using the root `Dockerfile`.
3. Add a **persistent volume** to the web service at exactly `/data`.
4. In this service's **Variables** tab for the Production environment,
   set `PersistentDataPath` to `/data`.
5. Apply pending configuration changes and deploy.
6. Check logs for an applied migration or "already up to date" followed by
   `Application started`.
7. Redeploy and verify "already up to date" without re-creating the schema.
8. Only after reviewing HTTPS and account-confirmation security, use a
   public HTTPS domain for controlled testing. Either configure real email
   delivery or explicitly enable demo-only unconfirmed accounts as below.
   Test authenticated add/edit/delete and verify saved book records after
   a further redeploy.
9. Share the verified **test URL** with David and Wadzanai only when ready.

## Controlled course demo registration (W05)

ASP.NET Core Identity normally requires email confirmation. The current
`IdentityNoOpEmailSender` does not send any messages, so ordinary new users
cannot complete confirmation in Production. The manual confirmation link is
now limited to Development and is never shown in Production.

For a **temporary course demonstration using throwaway accounts only**, set
`AllowUnconfirmedDemoAccounts=true` as a Railway service variable. This lets
new test accounts sign in without email confirmation. Do not enable this for
real users or sensitive data; anyone can register with an unverified email
address. Use artificial email addresses and unique test passwords, and turn
the variable off again after the demo. Password recovery by email still does
not work without a real email sender. The recommended long-term solution is
an actual email provider and verified addresses.

This opt-in setting does **not** validate Railway HTTPS, trusted proxy
settings, backups, or all browser workflows. Complete those checks before
marking RT-18 finished. Never put the test passwords in repository files.

## Where configuration and secrets belong

- Use Railway **service variables** in the right environment for connection
  strings, API keys and other runtime secrets; **never** commit actual values
  to GitHub, markdown, `appsettings.json` or screenshots.
- The current `PersistentDataPath=/data` setting is a **path**, not a secret.
- SQLite storage and Data Protection keyring are files on the mounted volume.
  Railway environment variables do **not** encrypt those files.
- If a real email provider is added, configure its credentials as Railway
  service secrets (or a dedicated secret manager), not in source control.
- For local development, use .NET User Secrets or local environment variables.
  Do not paste tokens, credentials, database contents or confirmation links
  into public issue comments or logs.
- Restrict Railway project access to authorized team members.
- Protect the Data Protection keyring at rest (e.g., managed key-encryption
  strategy) before using the service for real personal data; current
  file-based key persistence emits an unencrypted-key warning.
- Treat volume snapshots/backups and restoring them as a separate operational
  requirement. Do not assume that keeping a volume is a complete backup.

## Production security limitations (not yet resolved)

- ASP.NET Core Identity still uses **IdentityNoOpEmailSender**. It does
  **not** deliver real email confirmation links. The limited opt-in demo
  registration option described above is not verified-email registration.
- Review HTTPS termination and **forwarded headers / trusted proxy**
  handling on Railway before enabling a public domain. The local Docker
  warning about missing HTTPS port does not by itself validate the public
  proxy configuration.
- Data Protection XML keys persist on the volume but currently have no
  configured XML encryptor. This is not suitable as-is for sensitive
  production user data.
- Full browser validation using two real accounts, saved books, and a
  Railway redeploy is still pending until the frontend pages exist.

## Completion boundary

**RT-06 (W03 infrastructure verification):** Host selected, .NET runtime
started, cloud database migration history persisted across a redeploy,
configuration location documented, and deployment steps written.

**Not covered by RT-06:** The W05 first fully working cloud version and
the W06 production/security/readiness testing. Do not mark those later
tasks completed based only on Railway startup logs.
