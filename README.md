
# Order Management System

Simplified Order Management System that uses a REST API in .net 6.
## Design
- Clean Architecture using CQRS(MediatR) with FluentValidation
- SOLID principles
- Design patterns: Dependency Injection (DI), Repository Pattern and Unit of work
- Entities: Customer, Product and Order
## Stack

**Back-end:** .Net 6 (Asp.net Core Identity/JWT, EF Core, MediatR/Fluent Validation, Serilog and xUnit) and SQL Server

## Installation

1.) Run the following scripts from the "Database" folder:
- 01_CreateDatabase.sql (create the tables and initial data)
- 02_CreateOrder.sql (stored procedure to create orders)

2.1) Set the database connection string in the appSettings.json in API and Test projects.

2.2) Set-up Serilog configuration, it is set to write in the "log" folder in the root folder.
Reference: https://procodeguide.com/programming/aspnet-core-logging-with-serilog/

3.) Start the application for that it creates the users below:
- admin@gmail.com
- manager@gmail.com
Their passwords are in the appSettings.json as the "SeedUserPW".

4.) Use the Postman collection for testing that is in the folder "Postman". 
## Features

- Products: Get(Search with Pagination and Sorting),Get by Id, Create, Update and Delete
- Orders: Get by Id and Create
- Customer: Search (Search with Pagination and Sorting)
- Auth: Login and User Registration
**Observation**: The Product search sorting is in the "tradicional" way with query parmeters and the Customer uses System.Linq.Dynamic.Core for sorting as POST.
## Unit Tests
- Product: Validate get product by ID when product not found and valid product ID
- Order: Test API and Data Access(repository) of create and also test invalid customer  and product
- Database: Test create Order stored procedure
## Improvements
- Complete CRUD for Customers and Orders
- Consider extra validation when deleting Customer, Product and Order for data integrity
- Complete unit tests for all use cases