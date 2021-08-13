# WebApiTaskList

**Rest API for creating and managing of TOWATCH/TOREAD lists based on APS.NET**

## How to build it?

With installed globally dotnet core call `dotnet build` from the project directory.

## How to run development server?

With installed globally dotnet core call `dotnet run` from the project directory.

## How to make migrations and apply them?

Call `dotnet ef migrations add [name of migration]` from the project directory to create
and `dotnet ef databse update` to update the database.

## How to examinate it works?

After running the development server you will get a string with your localhost:[port], which is being listened by it.

Open your browser and open `localhost:[port]/swagger` (the __port__ is __5000__ by default, so the direct link is [here](localhost:5000/swagger)).

Now you **swagger**'s web interface, from where you can test any request.

## What is it?

It's a MVC-based rest api application, providing the following functionality:

__you can check full OpenAPI specification [here](swagger.yml)__

For __List__ entity:

1. Create __List__
2. Get all __Lists__ with extra parameters
3. Get a specific __List__ by id
4. Update __List__'s data
5. Merge to __Lists__ into one
6. Delete __List__

For __Task__ entity:

1. Create __Task__
2. Get all __Tasks__ with extra parameters
3. Get a specific __Task__ by id
4. Update __Task__'s data
5. Merge to __Tasks__ into one
6. Delete __Task__

The MVC model in this implementation is represented in:

1. Controllers (which are actually view part of our application)
2. Services, that provides business-logic
3. Repositories, that "communicate" with our database
4. Models, that represnt our entities for both sides: controllers and repositories
5. Global database context, which is supposed to work with models via EntityFramework

The layers are interconnected with dependecy injection. See more [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0).

The actual application is configured to use sqlite and provides the database in the same repository,
but it can be changed to any other SQL server of choice. See more [here](https://docs.microsoft.com/en-us/ef/ef6/).

## Some misc:

**IIS Express** and **DB Browser for sqlite** are useful tools for debugging on Windows.
Unfortunately, I haven't found any analogue for linux :(
