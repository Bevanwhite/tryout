﻿Programme structure 
1. Solution can have multiple projcets
2. if you double click on `NZWalks.API`
	* this is a `*.csproj` file
	* we can see some properties in this file
	* when you add nuget packages it will be a refference in here.
3. `launchSettings.json` this file comes under properties folder
	* launch related details are here.
	* application Url are keep in this file 
4. `appsettings.json`
	* use to store confiugartion
	* like loglevel, connection strings database, envoirment specific details
5. `Program.cs` 
	* entrypoint of the application
	* this file is running first
	* you can add dependencies to our application
	* you can do dependency injection 


what is Rest?
	* style of architecture for building web services
	* rest is set of principles that how web services should be design and interact with each other
	* Rest is based on resources.
	* server should not state any client state between this request.
	* client should provide all the necessary things in each request.
	* it should be able handle multiple request because no need to state the client state

why .net core? 
	* Http verbs
	* Routing
	* Model binding
	* content negotiation
	* Response types

what are http verbs?
	* http verbs defines the type actions that can be performed on a resource identified by a URI
	* GET		-	
	* POST		-
	* PUT		-
	* DELETE	-
	* PATCH		-
	* OPTIONS	-


how add the database and the migration
	* `Add-Migration "Initial Migration"`
	* `Update-Database`

what are DTO ? 
	* DTO stands for Data Transfer Objects
	* used to transfer data between different layers
	* Typically contain a subsetof the properties in the domain model
	* For example transferring data over a network

Advantages of DTOs
	* Separation of concerns
	* performance
	* security
	* Versioning

what is Async Programming?
	* technique that enables your program to start a potentially long-running task and still be able 
	  to be responsive to other events while that task runs, rather than having to wait until that task
	  runs, rather than having to wait until that task has finished. 

what is a Repository Pattern ?
	* it is helping to data access layer seprate from the rest of the application
	* provides interface without exposing implementation
	* helps create abstraction

what are the benefits
	* Decoupling
	* Consistency
	* Performance
	* Multiple data Sources(switching databases)

what is AutoMapper
	* object-to-object mapping
	* simplification
	* Map between DTOs and Domain Models and vice versa
	* quite powerful apart from  just simple object-to-object mapping

what are Navigation Properties?
	* Allow to Navigate from one entity to another
	* Walks Domain model will have Region Navigation Property