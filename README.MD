# Alten Challenge - Cancun Hotel API

This is the repository with my solution for the Alten Challenge



## Table of Contents

[About](#About)

[Project Structure](#Project-Structure)

[Tech Used](#Tech-Used)

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

## Tech Used

* [.NET 6.0](https://dotnet.microsoft.com/en-us/download)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [Npgsql](https://www.npgsql.org/index.html)

* [PostgreSQL](https://www.postgresql.org/)

* [xUnit](https://xunit.net/)
* [Bogus](https://github.com/bchavez/Bogus)
* [Moq](https://github.com/moq/moq4)
* [Moq.AutoMocker](https://github.com/moq/Moq.AutoMocker)
* [FakeItEasy](https://fakeiteasy.github.io/)
* [Fluent Assertions](https://fluentassertions.com/)
* [Fluent Validation](https://fluentvalidation.net/)