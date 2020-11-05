To add a migration:

From the Domain folder, run:  
`dotnet ef --startup-project ..\Web migrations add <Migration name> --context ReferenceDataContext -o Infrastructure\EntityFramework\Migrations`