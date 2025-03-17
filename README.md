# NotificationAPI .NET Server SDK

NotificationAPI SDK for server-side (back-end) .NET projects.

* Free software: MIT license

## Docs

Please refer to our [documentation](https://docs.notificationapi.com).

## Contribution

### Prerequisites

1. Install .NET SDK 8.0 or later:
   - Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### Development Workflow

We use a Makefile to simplify common development tasks. Before submitting a PR, please ensure you follow these steps:

1. Restore dependencies:
```
make restore
```

2. Build the project:
```
make build
```

3. Run tests:
```
make test
```

4. Format the code:
```
make format
```

5. Verify code formatting:
```
make format-check
```

### Important Notes

- 100% code coverage is required for all new code.
- All code must pass the formatting check before a PR will be accepted.
- The GitHub Actions workflow will automatically verify formatting and run tests on your PR.

### Manual Commands

If you prefer not to use the Makefile, you can run the commands directly:

```
# Restore dependencies
dotnet restore src/NotificationApi.Server.sln

# Build the project
dotnet build src/NotificationApi.Server.sln --configuration Release --no-restore

# Run tests
dotnet test src/NotificationApi.Server.Tests --configuration Release --no-build --verbosity normal

# Format code
dotnet format src/NotificationApi.Server.sln

# Check formatting
dotnet format src/NotificationApi.Server.sln --verify-no-changes --verbosity diagnostic
```
