# Building my first web app with dotnet core on Ubuntu 16.04

## intro
Attempting to build an example website using
- asp net core
- ef core with ef migrations
- yeoman
- postgresql

## references
https://www.microsoft.com/net/core#linuxubuntu
https://docs.microsoft.com/en-us/aspnet/core/client-side/yeoman
https://andrewlock.net/adding-ef-core-to-a-project-on-os-x/
https://www.digitalocean.com/community/tutorials/how-to-install-and-use-postgresql-on-ubuntu-16-04

## steps to re-create this project from scratch

1. install everything
```
sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
sudo apt-get update
sudo apt-get install dotnet-dev-1.0.1 nodejs npm postgresql postgresql-contrib
sudo npm install -g yo bower generator-aspnet
```

2. configure postgresql
```
# create a test user + password
sudo -u postgres createuser --superuser <username>
sudo -u postgres psql -c "alter role <username> with password '<password>'"
createdb <username>
```

3. create a new web app
```
yo aspnet
- Web application basic [without membership/auth]
- Bootstrap
- MyWebApp
cd MyWebApp
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
```
- add Microsoft.EntityFrameworkCore.Tools.DotNet to .csproj (see this project)
- add DB connection string to appsettings.Development.json (see this project)
- inject DB context in Startup.ConfigureServices (Startup.cs)
- run `dotnet restore`

4. Add a model, create a migration, apply to database
- see models under Models/
- run `dotnet ef migrations add <migration name>`
- remove the erroneous `using ;` in the generated .Designer and *ModelSnapshot.cs files
    + to be fixed in EF core: https://github.com/aspnet/EntityFramework/issues/2467
- run `dotnet ef database update`

5. build and run
```
dotnet build
dotnet run
```
- server now running on http://localhost:5000
- stop it with ctrl-c

## todo
- go through the asp.net generic readme that came with this project (below)
- ef in memory provider for testing?

## notes
- ef migrations seem to be tied to DB provider. That's not good, right?
  eg. using postgres, initial migration uses NpgsqlValueGenerationStrategy

------------------------------------------------------------------

# Generic ASP.NET readme
# Welcome to ASP.NET Core

We've made some big updates in this release, so it’s **important** that you spend a few minutes to learn what’s new.

You've created a new ASP.NET Core project. [Learn what's new](https://go.microsoft.com/fwlink/?LinkId=518016)

## This application consists of:

*   Sample pages using ASP.NET Core MVC
*   [Bower](https://go.microsoft.com/fwlink/?LinkId=518004) for managing client-side libraries
*   Theming using [Bootstrap](https://go.microsoft.com/fwlink/?LinkID=398939)

## How to

*   [Add a Controller and View](https://go.microsoft.com/fwlink/?LinkID=398600)
*   [Add an appsetting in config and access it in app.](https://go.microsoft.com/fwlink/?LinkID=699562)
*   [Manage User Secrets using Secret Manager.](https://go.microsoft.com/fwlink/?LinkId=699315)
*   [Use logging to log a message.](https://go.microsoft.com/fwlink/?LinkId=699316)
*   [Add packages using NuGet.](https://go.microsoft.com/fwlink/?LinkId=699317)
*   [Add client packages using Bower.](https://go.microsoft.com/fwlink/?LinkId=699318)
*   [Target development, staging or production environment.](https://go.microsoft.com/fwlink/?LinkId=699319)

## Overview

*   [Conceptual overview of what is ASP.NET Core](https://go.microsoft.com/fwlink/?LinkId=518008)
*   [Fundamentals of ASP.NET Core such as Startup and middleware.](https://go.microsoft.com/fwlink/?LinkId=699320)
*   [Working with Data](https://go.microsoft.com/fwlink/?LinkId=398602)
*   [Security](https://go.microsoft.com/fwlink/?LinkId=398603)
*   [Client side development](https://go.microsoft.com/fwlink/?LinkID=699321)
*   [Develop on different platforms](https://go.microsoft.com/fwlink/?LinkID=699322)
*   [Read more on the documentation site](https://go.microsoft.com/fwlink/?LinkID=699323)

## Run & Deploy

*   [Run your app](https://go.microsoft.com/fwlink/?LinkID=517851)
*   [Run tools such as EF migrations and more](https://go.microsoft.com/fwlink/?LinkID=517853)
*   [Publish to Microsoft Azure Web Apps](https://go.microsoft.com/fwlink/?LinkID=398609)

We would love to hear your [feedback](https://go.microsoft.com/fwlink/?LinkId=518015)
