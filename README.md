# Overview

Web app template by the Microsoft Devices Software Experiences team.

# Updates

# Frontend Architecture

- React with TypeScript
- nswag-generated DTOs and client for backend API

# Backend Architecture

- ASP.NET Core 3.1
- Uses MediatR as CQRS implementation
- Uses AutoMapper to handle entity-to-DTO mapping
- Unit and integration tests using Moq, MSTest, and Mvc.Testing

# How to run locally

This setup is for Windows users but this solution will work cross-platform (Mac/Linux).

1. Install Visual Studio 2019 Enterprise (for backend).
2. Install .NET Core SDK 3.1 for VS2019.
3. Install Visual Studio Code (for frontend).
4. Open the `.sln` file and wait for nuget package restore.
5. Open **Test Explorer** and click "Run All" to execute test suite.
6. Press F5 button on your keyboard to launch the backend in debugging mode and your browser should open `https://localhost:44345/swagger`.
7. Open `client` folder in VS Code.
8. In the **Terminal**, execute these 2 commands:

```
npm install
npm start
```

9. The webpack dev server should host the frontend and your browser should open `http://localhost:3000`.

# Adding an Entity Framework Core migration

1. Open a command prompt in the **Microsoft.DSX.ProjectTemplate.Data** folder.
2. `dotnet ef migrations add <NAME OF MIGRATION>`

# Removing the latest Entity Framework Core migration

1. Open a command prompt in the **Microsoft.DSX.ProjectTemplate.Data** folder.
2. `dotnet ef migrations remove`

# Contributing

This project welcomes contributions and suggestions. Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
