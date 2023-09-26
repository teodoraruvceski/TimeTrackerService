# Define the path to your ASP.NET Core project folder
$projectPath = "D:\teodora\Projects\TimeTrackerService\TimeTrackerService\TimeTrackerService"

# Check if migrations exist
$migrationsExist = Test-Path -Path "$projectPath\Migrations"

if ($migrationsExist) {
    dotnet tool install --global dotnet-ef
    Write-Host "Migrations exist. Applying database updates..."
    # Execute migrations to update the database
    dotnet ef database update --project "$projectPath" --startup-project "$projectPath"
} else {
    Write-Host "No migrations found. Skipping database update."
}