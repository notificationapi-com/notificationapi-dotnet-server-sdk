.PHONY: restore build test format format-check

restore:
	dotnet restore src/NotificationApi.Server.sln

build: restore
	dotnet build src/NotificationApi.Server.sln --configuration Release --no-restore

test: build
	dotnet test src/NotificationApi.Server.Tests --configuration Release --no-build --verbosity normal

format:
	dotnet format src/NotificationApi.Server.sln

format-check:
	dotnet format src/NotificationApi.Server.sln --verify-no-changes --verbosity diagnostic 