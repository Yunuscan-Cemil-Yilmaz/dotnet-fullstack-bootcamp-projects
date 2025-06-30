## how to create a ASP.NET MVC project ? 
check your installitions firstly:
dotnet --version
dotnet --list-sdks
dotnet --list-runtimes

if u dont install yet:

sudo apt update
sudo apt install -y wget apt-transport-https software-properties-common
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

## Install .NET SDK 
sudo apt update
sudo apt install -y dotnet-sdk-8.0

## install LibMan tool
dotnet tool install -g Microsoft.Web.LibraryManager.Cli

## create project
for develop:
dotnet new mvc -n ProjectName
for run:
dotnet build
dotnet run


