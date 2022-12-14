# Alten Challenge - Cancun Hotel API

This is the repository with my solution for the Alten Challenge

[![Build and deploy ASP.Net Core app to Azure Web App - cancun-hotel-api](https://github.com/tassan/alten-challenge-api/actions/workflows/main_cancun-hotel-api.yml/badge.svg)](https://github.com/tassan/alten-challenge-api/actions/workflows/main_cancun-hotel-api.yml)

## Table of Contents

[About](#about)

[Project Structure](#project-structure)

[Tech Used](#tech-used)

[How to Use](#how-to-use)

[Deployment](#deployment)

[Goals to achieve 99.99% Uptime](#goals-to-achieve-9999-uptime)

[Future Improvements](#future-improvements)

## About

This API is fort he fictional very last hotel in Cancun, the hotel IT team will manage it.  This is a RESTful API with the basic CRUD operations, the enduser is capable of creating and control customers, reservations and check reservations availability.

## Project Structure

The project is structured in layers to segregate the responsibilities, with this current structure, its a good starting point, its possible to make new projects or even adapt to microservices later. This is structure is based on my knowledge and studies in DDD.

* **Application**: The Application layer is responsible to apply the business rules and validation. The input contract (view models) are defined here. The services important to the business, in this case, Customer Management and Booking Management are located in this layer.
* **Domain**: The Domain layer holds the business objects, attributes and their relationships.  The validation rules who are applied by the Application layer are defined here in the domain. 
  * **Note**: *Most of the business objects in this project can be considered "anemic", since they mostly only hold data, instead of modify their states. This is something that can be improved if necessary.*
* **Infra (Infrastructure)**: The Infra layer enables the project to interact with external storages. This layer is know as the persistence layer too. The use of ORM is configured in this layer, this project uses EF Core, but other ORMs like Dapper could be used, or, even change the structure to a Non Relational storage like MongoDb. This layer provides the Repositories implementation, based on the interface contract located in the Domain.
* **Services**: The Services layer is the application's boundary, it defines the available operations and responses for each of them. The API its located here with its endpoints, as a example, this layer could have a Console App service for the user to input data, instead of an Web API.
* **Tests**: The Tests layer holds every test necessary validate business objects, the application services and even infrastructure. Unit Tests, Integration Tests and Regression tests can be stored in this layer.

## How to Use

1. Clone the Repository

```shell
git clone https://github.com/tassan/alten-challenge-api.git
```

2. Configure database connection in the appsettings.json file

```json
 "ConnectionStrings": {
    "DefaultConnection": "Server=[server];Port=[port];Database=[database];User Id=[user];Password=[password];"
  },
```

*Change the values to your Postgresql connection*

3. Restore project package, in the root folder use the command:

```shell
dotnet restore
```

4. Build the project, in the root folder use the command:

```shell
dotnet build
```

5. Run the tests (Optional), in the root folder use the command:

```shell
dotnet test
```

6. Run the project Migrations

```shell
dotnet ef database update --project src\Infra\CancunHotel.Infra\CancunHotel.Infra.csproj --startup-project src\Services\CancunHotel.Services.API\CancunHotel.Services.API.csproj --context CancunHotel.Infra.Context.ApplicationContext --configuration Debug "20220831003919_Remove deleted field"
```
*This command use the last migration file in project*

7. Run the project, in the root folder use the command:

```shell
dotnet run
```

The project uses port `7236` for HTTPS and `5255` for HTTP in `localhost`. If you want, you can change these ports in `launchSettings.json` in the `CancunHotel.Service.API`.

## Deployment

This project is configured to auto-deploy on Azure every time a new push is made to the `main` branch of this repo.

The deployed API uses a postgresql hosted on heroku.

You can see the deployed version here [Swagger UI](https://cancun-hotel-api.azurewebsites.net/swagger/index.html).

## Goals to achieve 99.99% Uptime

Preliminary tests (including failed/excluded) of the current state of the API, hosted in Azure, gave me an average 79.71% of availability.

![Imgur](https://i.imgur.com/pEfaHYr.png)

An 99.99% ("4 nines") as it can be seen in [Google's Availability Table](https://sre.google/sre-book/availability-table/), the "4 nines" require a downtime of less than 60 min per year.

![Imgur](https://i.imgur.com/BbiUZWz.png)

To achieve that kind of performance, some of the changes who could help are:

* First, it would be necessary for the IT team responsible for the API to define strategies for deploying, maintaining and developing the API;
* The continuous use of monitoring apps and health-checks, like Application Insight in Azure (which is implemented in the deployed version);
* The implementation of distributed cache to save data who isn't going to change so often;
* Apply Load balancers to orchestrate network traffic;
* Have more instances of the API behind a gateway with a policy to remove the ones who are unavailable in the pool;
* Have more than one deploy slot to avoid downtime during deploy;
* Containerize the application would allow us to apply much of the other topics above too;
* The project structure allow us to use different databases, in case this becomes a bottleneck to the project, it could be changed. Like the API, the database could have more instance too, as long we guarantee that the data is consistent in all of them;
* Use [AspNetCoreRateLimit](https://github.com/stefanprodan/AspNetCoreRateLimit) to rate limit requests to the API that a client can make would help keep the API more secure and avoid downtime.

## Future Improvements

A list of improvements I'd like to have done if it was possible:

* The unit test coverage right covers only 45-50% of the code. Increasing the coverage its very important;
* Improve the deploy strategy:
  * Deploy only when the code was changed instead of every push to the `main` branch;
  * Deploy only when every Unit Test passes;
  * Deploy of different environments like DEV and QA.
* Improve the logging;
* Improve the project structure:
  * Implement Mediator pattern;
  * Implement CQRS pattern;
* Switch from Heroku Postgres, since its free and its gonna be deprecated soon;
* Auto apply EF Core migrations.

## Tech Used

* [.NET 6.0](https://dotnet.microsoft.com/en-us/download)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [Npgsql](https://www.npgsql.org/index.html)
* [PostgresSQL](https://www.postgresql.org/)
* [xUnit](https://xunit.net/)
* [Bogus](https://github.com/bchavez/Bogus)
* [Moq](https://github.com/moq/moq4)
* [Moq.AutoMocker](https://github.com/moq/Moq.AutoMocker)
* [FakeItEasy](https://fakeiteasy.github.io/)
* [Fluent Assertions](https://fluentassertions.com/)
* [Fluent Validation](https://fluentvalidation.net/)
* [Azure](https://azure.microsoft.com/)