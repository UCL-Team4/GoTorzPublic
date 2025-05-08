# GoTorz

A sample travel packages website built with **Blazor Server** and **.NET 9.0**, designed to showcase modern web development practices and architectural patterns. This project is for educational purposes and is _not_ intended for production use.

---

## Table of Contents
- [GoTorz](#gotorz)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Prerequisites](#prerequisites)
  - [Getting Started](#getting-started)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
  - [Testing](#testing)
  - [CI/CD](#cicd)
  - [License](#license)

---

## Features
- **Blazor Server**: Responsive and interactive UI with real-time data updates.
- **.NET 9.0**: Leverages the latest framework for performance and security enhancements.
- **Travel Packages**: Browse, search, and filter a variety of travel packages with detailed descriptions and pricing.
- **Entity Framework Core**: Code-first ORM for robust data access and migrations.
- **Dependency Injection**: Built-in DI for clean architecture and easier testing.
- **Unit Testing**: Unit tests to validate business logic and data operations.

## Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- SQL Server or any compatible database engine

## Getting Started
1. **Clone the repository**
   ```bash
   git clone https://github.com/UCL-Team4/GoTorzPublic
   cd GoTorzPublic
   ```
2. **Open the solution** in your IDE of choice.

## Configuration
- Open `appsettings.json` to configure database connection strings and other settings as needed.

## Running the Application
1. Run the following commands in your Product Manager Console to apply migrations:
   ```bash
   Add-Migration InitialCreate
   ```
    ```bash
    Update-Database
    ```
2. Build and run the application using your IDE of choice.
3. Visit `http://localhost:5000` in your web browser to view the application.

## Testing
The project includes various unit tests to ensure functionality and reliability. You can view and run these tests using the built-in test runner in Visual Studio or by using the command line:
```bash
dotnet test
```

This will execute all tests in the solution and provide a summary of the results.

## CI/CD
We have also includes a ready-to-use GitHub Actions workflow for continuous integration. On every push or pull request to the master branch, the pipeline automatically restores dependencies, compiles the application, and runs the test suite using the latest .NET 9.0 preview. This can also easily be extended to add deployment, for any further details you can find the workflow file at .github/workflows/dotnet.yml.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.