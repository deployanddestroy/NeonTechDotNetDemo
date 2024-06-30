global using NeonTechDotNetDemo;
using Microsoft.Extensions.Configuration;

// 1. Add the appsettings.json file via IConfiguration
IConfiguration configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.Build();

// 2. Instantiate a new NeonConfig object and bind
//    properties from NeonConnection to it
var neonConfig = new NeonConfig();
configuration.GetSection("NeonConnection").Bind(neonConfig);

// 3. Instantiate a new NeonContext, passing in the 
//    above config. This is how we access the DB.
var connection = new NeonContext(neonConfig);

// 4. Initiate the connection and create the tables!
await connection.Init();
