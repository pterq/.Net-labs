/*
Użyte komendy w terminalu do stworzenia modelu na podstawie bazy SchoolDb:

mkdir Lab03_6_Core
cd Lab03_6_Core

dotnet new console
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolDb;Integrated Security=True;" Microsoft.EntityFrameworkCore.SqlServer -o Model

 */