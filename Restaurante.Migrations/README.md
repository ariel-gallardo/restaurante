* Add Migration
dotnet ef migrations add InitialMigration --project Restaurante.Migrations --startup-project Restaurante.API
* Add Custom migration
dotnet ef migrations add nameOfMigration --project Restaurante.Migrations --startup-project Restaurante.API
* Update Database
dotnet ef database update --project Restaurante.Migrations --startup-project Restaurante.API

