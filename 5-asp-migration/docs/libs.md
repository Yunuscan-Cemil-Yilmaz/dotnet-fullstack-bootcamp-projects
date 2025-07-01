dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add package BCrypt.Net-Next --version 4.0.2



# check the versions :
dotnet list package --outdated


# Truthly versions ( generally not always !! (for ef))
for .NET 8 => 8.x.x
for .NET 7 => 7.x.x
for .NET 6 => 6.x.x