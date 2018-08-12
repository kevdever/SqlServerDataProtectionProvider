# SqlServerDataProtectionProvider
A .Net Core Data Protection provider to persist keys to Sql Server

The primary public use of this library will be via the PersistKeysToSqlServer() IDataProtectionBuilder extension method.

Usage:
In your database context, implement IDataProtectionContext and add a DbSet<DataProtectionKey> property.
```
    public class ApplicationDbContext : IDataProtectionContext
    {
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    ...
    }
```
  
In Startup.cs's ConfigureServices method, add a service to provide the IDataProtectionContext:
`services.AddTransient<IDataProtectionContext, ApplicationDbContext>();`
  
Also in your startup's ConfigureServices method, when configuring DataProtection, you can now use PersistToSqlServer to persist your keys to a Sql Server database.  You will need to provide the 

Example:
```
  services.AddDataProtection()
      .PersistKeysToSqlServer(() => services.BuildServiceProvider().GetRequiredService<IDataProtectionContext>())
      .ProtectKeysWithCertificate(dataProtectionCert)
      .SetApplicationName("<AppName>")
      .SetDefaultKeyLifetime(TimeSpan.FromDays(10))
      .UseCryptographicAlgorithms(encryptionSettings);
```
