# Overview

Web app template by the Microsoft Devices Software Experiences team.

# Client (front-end)

- [React](https://reactjs.org/docs/getting-started.html) with [Redux](https://redux.js.org/introduction/getting-started) and [TypeScript](https://www.typescriptlang.org/docs)
- [Microsoft Fabric UI](https://developer.microsoft.com/en-us/fluentui#/get-started)
- [NSwag](https://github.com/RicoSuter/NSwag) generated DTOs and client for back-end API
- [MSAL-React](https://github.com/AzureAD/microsoft-authentication-library-for-js/tree/dev/lib/msal-react)
- [MSAL-Browser](https://github.com/AzureAD/microsoft-authentication-library-for-js)

# Service (back-end)

- [ASP.NET Core 3.1](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro)
- [MediatR](https://github.com/jbogard/MediatR) as [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs) implementation
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) handling Entity-to-DTO mapping
- Unit and integration tests using [Moq](https://github.com/moq/moq4) and [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [Swagger UI](https://github.com/swagger-api/swagger-ui)

# How to run locally

1. [Download and install the .NET Core SDK](https://dotnet.microsoft.com/download)
    * If you don't have `localdb` available on your system, [Download and install SQL Server Express](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
    * NOTE: We will remove the `localdb` requirement in a future PR
2. Open a terminal such as **PowerShell**, **Command Prompt**, or **bash** and navigate to the `service` folder
3. Run the following `dotnet` commands:
```sh
dotnet build
dotnet run --project Microsoft.DSX.ProjectTemplate.API
```
3. Open your browser to: `https://localhost:44345/swagger`.
4. In another terminal, navigate to the `client` folder and run the following `npm` commands:
```sh
npm install
npm start
```
5. The webpack dev server hosts the front-end and your browser will open to: `http://localhost:3000`

# Adding an Entity Framework Core migration

1. Open a command prompt in the **Microsoft.DSX.ProjectTemplate.Data** folder.
2. `dotnet tool install --global dotnet-ef`
3. `dotnet ef migrations add <NAME OF MIGRATION>`

# Removing the latest Entity Framework Core migration

1. Open a command prompt in the **Microsoft.DSX.ProjectTemplate.Data** folder.
2. `dotnet ef migrations remove`

# To-Do

1. Migrate to MSAL.js
2. Include Authentication and Authorization logic
3. Add Docker file
4. Move away from localdb
5. Create a CLI setup wizard

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
