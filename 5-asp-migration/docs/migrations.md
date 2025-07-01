# create model file
firstly create your model file in this path -> /Model/YourModelFile

# add your connection string in json files (appsettings.json and appsettings.Development.json)
add like this : 
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=poll_craft;user=yuncemaz;password=53231323;"
    // write your connection strings
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

# create your DbContext file
create file in this path -> /Infrastructure/Data/AppDbContext.cs
u can check this file in this project for how to write ? 


# update database

* change your model file 
* run this:
```bash
dotnet ef migrations add <ChangedTable>
dotnet ef database update
```