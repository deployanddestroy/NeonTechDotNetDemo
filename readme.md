## Neon Postgres with .NET 8.0

Example project used in a video demonstration of how to connect to a Neon Postgres DB with .NET 8.0.

Video: [Neon Postgres with .NET 8.0](https://www.youtube.com/watch?v=GQ5yOOz-IDE) on YouTube

### Notes
The setup process is pretty straightforward:
1. Pull in the database + server configuration from a config file/env variables
2. Create a database connection using above configuration
3. Run SQL commands via a context class against your Neon Postgres instance

To run the application and see it create tables in Neon, simply update `appsettings.json` with your Neon information.

Happy SQLing!