## Identity Razor

**1. Restore Package**
<br/>
dotnet restore

**2. Needed Tool**
<br/>
dotnet tool install --global dotnet-ef
<br/>
dotnet tool install -global dotnet-aspnet-codegenerator

**3. Migrate Database**
<br/>
dotnet ef migrations add Init
<br/>
dotnet ef database update

**4. Generate Identity View**
<br/>
dotnet aspnet-codegenerator identity -dc IdentityRazor.Data.IRContext

**5. Configue EmailSender**
<br/>
Configure in appsettings.json

Go To Setting in your Google Account and Enable IMAP

Visit : https://myaccount.google.com/lesssecureapps and switch to ON