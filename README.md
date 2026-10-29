# ClinicCare

ClinicCare is a clinic management web application with an Angular frontend and an ASP.NET Core Web API. It provides a foundation for managing clinic data, doctors, patients, specialties, medical services, schedules, and appointments.

> **Project status:** In development. A hosted demo and production deployment are not currently configured.

## Features

- Manage clinics, doctors, patients, and specialties
- Manage medical services and doctor schedules
- Create and manage appointments and their statuses
- View dashboard data
- REST API documented with Swagger in development

## Tech stack

| Area | Technologies |
| --- | --- |
| Frontend | Angular 22, TypeScript, Angular Material, PrimeNG |
| Backend | ASP.NET Core Web API, .NET 8 |
| Data | Entity Framework Core 8, SQL Server |

## Repository structure

```text
Backend/DBL/
  APIL/   ASP.NET Core API and controllers
  BLL/    Business services and DTOs
  DBL/    Entities, database context, and EF Core migrations
Frontend/Doctor/ Angular application
```

## Run locally

### Requirements

- .NET 8 SDK
- Node.js and npm
- SQL Server
- The EF Core command-line tool (`dotnet-ef`) for applying migrations

### 1. Configure the database

From the repository root, initialize .NET user secrets and set a connection string for your own SQL Server instance:

```bash
dotnet user-secrets init --project Backend/DBL/APIL
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=ClinicCareDb;Trusted_Connection=True;TrustServerCertificate=True;" --project Backend/DBL/APIL
```

Change the server and authentication settings to match your SQL Server setup. User secrets are stored locally and are not committed to Git.

### 2. Apply database migrations

```bash
dotnet ef database update --project Backend/DBL/DBL --startup-project Backend/DBL/APIL
```

### 3. Start the API

```bash
dotnet run --project Backend/DBL/APIL --launch-profile https
```

The configured HTTPS profile uses `https://localhost:7130`. In development, Swagger is available at `https://localhost:7130/swagger`.

### 4. Start the frontend

In a second terminal:

```bash
cd Frontend/Doctor
npm ci
npm start
```

Open `http://localhost:4200`. The frontend currently calls the API at `https://localhost:7130`; trust the local HTTPS development certificate if your browser prompts you.

## Project layout

The backend is split into three .NET projects: `APIL` hosts the API, `BLL` contains business services, and `DBL` contains persistence models and migrations. The Angular client lives in `Frontend/Doctor`.

## Contributing

Issues and pull requests are welcome. For a substantial change, please open an issue first to discuss the proposed work.

## License

No license has been added yet. All rights are reserved unless a license is added to this repository.

---

## عن المشروع

ClinicCare هو تطبيق ويب لإدارة العيادات، بواجهة Angular وواجهة خلفية ASP.NET Core Web API. يوفّر أساسًا لإدارة بيانات العيادة والأطباء والمرضى والتخصصات والخدمات الطبية وجداول الدوام والمواعيد.

**حالة المشروع:** قيد التطوير. لا توجد حاليًا نسخة تجريبية مستضافة أو إعدادات نشر للإنتاج.
