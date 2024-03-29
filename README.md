# DbMigrator

An attempt to create a migrations application using [DbUp](https://dbup.github.io/).

This project is just a PoC, just made to learn how to use the DbUp library.

It has been build using .Net Core 3.0 Preview 7.

---

## Build

You can do it with .Net CLI:

```powershell
dotnet restore
dotnet build
```

Or using Visual Studio (*Don't forget to restore the NuGet packages before build.*)

## Usage

First you have to place your Sql Scripts in the proper directory:

* PreDeployment
* Migrations
* PostDeployment

__This is IMPORTANT!__: Only the scripts placed in the Migrations directory are saved in the journal table. The scripts placed in both Pre and Post deployment directories are executed everytime the application runs. So be carefull what scripts do you place in those directories.

Once you place the scripts, just run the application.

The program takes 2 arguments.
The first one is the environment, that has 4 options:

* Development
* Testing
* Staging
* Production

The second argument is the action to perform, there are also 4 options:

* PreDeployment
* PostDeployment
* Migrations
* AllOperations

Example of usage:

Inside the project:

```powershell
dotnet run Development PreDeployment
```

Or after building the project:

```powershell
DbMigrator Development PreDeployment
```

> __*Note: The arguments are case sensitive.*__

The program also writes a log with all the operations that it performs.
You can change the file name and configuration in the appsettings.json file.
