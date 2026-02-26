/*
 
#1
Migration command in powershell/package mangager console:
add-migration InitialSchoolDB

projekt musi się kompilować bez błędów, żeby migracja przeszła pomyślnie


#2
utworzenie bazy danych za pomocą komendy:
Update-Database -Verbose
opcja -Verbose pokazuje logi podczas tworzenia bazy danych


#3
dodanie zmian w entity Student
add-migration "ModifiedStudentEntity" 

aktualizacja schematu bazy danych
update-database


#4
przywrócenie bazy danych do snapshotu "InitialSchoolDB"
Update-database "InitialSchoolDB"
nie zmienia nic w kodzie tylko robi zmiany w strukturze bazy danych


#5
wypisuje wszystki migracje/snapshoty
Get-Migration 

#6
usuwa ostatnią migrację jeśli nie jest zaaplikowana
remove-migration
zwróci exception jeśli migracja jest zaaplikowana


//7
//generuje skrtypt SQL dla bazy danych
//generate-script
//Script-Migration  <= to działało w .net 10
//skrypt zapisuje się w /obj/Debug/net10.0/  z roszrzeniem .sql


8
instlacja .net core cli w terminalu/powershell
dotnet tool install --global dotnet-ef
dotnet ef --help   <=dostępne komendy

pozwala na zarządzanie:
- bazą danych: dotnet ef database
- dbcontext: dotnet ef dbcontext
- migrcjami: dotnet ef migrations

dotnet ef [komenda] --help
dla wyjaśnienia wybranej komendy




 */

public class Program
{
    static void Main(string[] args)
    {

    }
}