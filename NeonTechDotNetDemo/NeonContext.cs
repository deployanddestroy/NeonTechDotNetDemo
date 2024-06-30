using Dapper;
using Npgsql;
using System.Data;

namespace NeonTechDotNetDemo;

/// <summary>
/// Context used to create a database connection and execute 
/// SQL commands against the DB.
/// </summary>
internal class NeonContext
{
	private NeonConfig _config;

	public NeonContext(NeonConfig config)
    {
		_config = config;
    }

	/// <summary>
	/// Creates connection object used to interact with the database.
	/// </summary>
	/// <returns></returns>
	private IDbConnection CreateConnection()
	{
		var connectionString = $"Host={_config.Server};Database={_config.Database};Username={_config.Username};Password={_config.Password}";
		return new NpgsqlConnection(connectionString);
	}

	/// <summary>
	/// Instantiates a 
	/// </summary>
	/// <returns></returns>
	public async Task Init()
	{
		await _initDatabase();
		await _createTables();
	}

	/// <summary>
	/// Creates the database if it doesn't exist.
	/// </summary>
	/// <returns></returns>
	private async Task _initDatabase()
	{
		using var connection = CreateConnection();
		var dbCountQuery = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_config.Database}';";
		var dbCount = await connection.ExecuteScalarAsync<int>(dbCountQuery);
		if (dbCount == 0)
		{
			var createDbQuery = $"CREATE DATABASE \"{_config.Database}\"";
			await connection.ExecuteAsync(createDbQuery);
		}
	}

	/// <summary>
	/// Creates the tables in the database.
	/// </summary>
	/// <returns></returns>
	private async Task _createTables()
	{
		// First categories
		await _initCategories();
		// Then products (depend on categories)
		await _initProducts();
	}

	/// <summary>
	/// Create categories table
	/// </summary>
	/// <returns></returns>
	private async Task _initCategories()
	{
		using var connection = CreateConnection();
		var sql = @"
			CREATE TABLE IF NOT EXISTS Categories (
				Id SERIAL PRIMARY KEY,
				Name VARCHAR(255) NOT NULL,
				Description VARCHAR,
				CreatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP
			);
		";
		await connection.ExecuteAsync(sql);
	}

	/// <summary>
	/// Create products table
	/// </summary>
	/// <returns></returns>
	private async Task _initProducts()
	{
		using var connection = CreateConnection();
		var sql = @"
			CREATE TABLE IF NOT EXISTS Products (
				Id SERIAL PRIMARY KEY,
				Name VARCHAR(255) NOT NULL,
				CategoryId INT REFERENCES Categories(Id),
				Description VARCHAR,
				PricePerUnit DECIMAL(10, 2),
				Active BOOLEAN DEFAULT FALSE,
				CreatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP
			);
		";
		await connection.ExecuteAsync(sql);
	}
}
