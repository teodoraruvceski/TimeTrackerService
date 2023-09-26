# Build ASP.NET Core App
Write-Host "Building ASP.NET Core App..."
cd "TimeTrackerService" # Replace with the actual path to your ASP.NET Core app folder
dotnet build


Write-Host "Running ASP.NET Core App with IIS..."
& "C:\Program Files (x86)\IIS Express\iisexpress.exe" /path:"." /port:44385 
