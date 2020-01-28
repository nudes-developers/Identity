# Nudes.Identity
Nudes.Identity is an ASP.Net Core Library to include all basic OAuth2 + OpenIdConnect basic functionality such as:
- Authorization Code Flow
- Consent Grant
- Consent Management
- Device Management

//TODO:Add influences

## Installation
Use the package manager [nuget](https://nuget.org) to install Nudes.Identity
```powershell
PM> Install-Package {//TODO:}
```

or

```powershell
> dotnet add package {//TODO:}
```

## Usage
To use `Nudes.Identity` will need to also use `IdentityServer4` and `MediatR`


### Set up identity server
First you need to setup your identity server configuration

On `ConfigureServices` at Startup.cs
```csharp
services.AddIdentityServer()
    .YourSpecificProjectConfigurations();
```

On `Configure` at Startup.cs
```csharp
app.UseIdentityServer();
```

### Set up Nudes.Identity
On `ConfigureServices` at Startup.cs
```csharp
services.AddControllerWithViews()
        .AddNudesIdentity();
```

### Set up AspnetCore Authentication

All Nudes.Identity pages work based on cookie authentication using `"Nudes.Identity"` schema so we gotta set it up

On `ConfigureServices` at Startup.cs
```csharp
using Nudes.Identity.Options;
//..//
services.AddAuthentication("Bearer") //or your specific authentication schema
        .AddCookie(NudesIdentityOptions.NudesIdentitySchema)
        .AddJwtBearer(op => /*...*/ ); //with your custom configuration
```

### Set up the authentication middleware
> It must be after `app.UseIdentityServer()`

On `Configure` at Startup.cs
```csharp
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
```

### Set up MediatR and Implement your ValidateUserCredentialsHandler
if you are not familiar with `MediatR` you can read all about it [here](https://github.com/jbogard/MediatR)

this handler will be used by the authorization logic to decide if it should authenticate an user or not, so you must implement your user data getting and password validation here

On any file
```csharp
public class ValidateUserCredentialsHandler : IRequestHandler<ValidateUserCredentialsQuery, UserResult>
    {
        public Task<UserResult> Handle(ValidateUserCredentialsQuery request, CancellationToken cancellationToken)
        {
            if (request.Username == "bob" && request.Password == "bob")
            {
                return Task.FromResult(new UserResult()
                {
                     Username = "bob",
                     SubjectId = "1",
                });
            }
            return Task.FromResult<UserResult>(null);
        }
    }
```

On `ConfigureServices` at Startup.cs
```csharp
using MediatR;

services.AddMediatR(this.GetType().Assembly);
```

## Front
//TODO:

## Views
These are the following views that are avaiable
- Account/Login
- Account/Logout
- Consent
- Device
- Device/UserCodeCapture
- Device/Callback
- Grants
- Grants/Revoke
- Home
- Home/Error
- External/Challenge
- External/Callback