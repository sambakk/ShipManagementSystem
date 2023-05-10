Ship Management Backend
=======================

This project provides a RESTful API for managing ships.

Installation
------------

To use this project, you will need to have .NET 6.0 installed on your machine.

1.  Clone the repository: 
```
git clone https://github.com/sambakk/ShipManagementSystem.git
```
2.  Navigate to the project directory:
```
cd ShipManagementSystem/ShipManagementBackend
```
3.  Build the project:
```
dotnet build ShipManagement
```

Usage
-----

To start the API server, run the following command:

```
dotnet run --project ShipManagement
```

You can then make HTTP requests to the server using a tool like `curl`, Postman or a web browser.

![Swagger REST API](https://i.ibb.co/k8H7dML/RESTAPI.png)

## Endpoints

The following endpoints are available:

#### GET /api/v1/ships

Returns a list of all ships in the system.

#### GET /api/v1/ships/{id}

Returns details for a specific ship.

#### POST /api/v1/ships

Creates a new ship in the system.

#### PUT /api/v1/ships/{id}

Updates an existing ship in the system.

#### DELETE /api/v1/ships/{id}

Deletes a ship from the system.


Testing
-------

To run the tests for this project, run the following command:


```
dotnet test ShipManagement.Test
```

This will run all tests in the project and output the results to the console.

To get code coverage run this command
```
cd ShipManagement.Test
coverlet bin/Debug/net6.0/ShipManagement.Test.dll --target dotnet --targetargs "test --no-build"
```

Author
-------

Abdessamad Bakkach
