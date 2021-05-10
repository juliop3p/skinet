# E-Commerce Skinet

## Udemy Course - Creating an E-Commerce with .Net Core + Angular

### Learning 

 - [ ] C# Generics
 - [ ] Repository and Unit of Work Pattern
 - [ ] Specification Pattern
 - [ ] Caching
 - [ ] Angular Lazy loading
 - [ ] Angular Routing
 - [ ] Angular Reactive Forms
 - [ ] Angular Creating a MultiStep form wizard
 - [ ] Accepting payments using Stripe
 - [ ] Angular Re-usable form components
 - [ ] Angular validation and async validation

**Useful Commands Learned**

- dotnet new <type of project>
- dotnet build -> build de project
- dotnet run -> run the current selected project
- dotnet restore -> to use after installing a package
- dotnet new sln --name <nome do projeto> -> create a new solution
- dotnet new webapi -n application -> create a web api project
- dotnet new webapi -n application -o Api.Application --no-https -> create a web api project (-o to name the output folder)
- dotnet sln add Api.Application -> Add a project to the solution
- dotnet new classlib -n Domain -o Api.Domain -f netcoreapp3.1  -> create a simple class project (-f to set the version .Net)
- dotnet add Api.Data/ reference Api.Domain/ -> to refence a project
- dotnet ef migrations add UserMigrations -> create migrations
- dotnet ef database update -> update the database with the current migrations
- dotnet dev-certs https -t -> Make the certified valid
- dotnet watch run -> hot reload
- dotnet tool list -g -> list the current tools like dotnet ef
