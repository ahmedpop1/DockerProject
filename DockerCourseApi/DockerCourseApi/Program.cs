using System.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());

app.MapGet("/podcasts", async () =>
{
    try
    {

        var db = new SqlConnection("Server=tcp:database;Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

        return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);
    }
    catch (Exception ex)
    {

        return new List<string>
         {
             "test Unhandled Exception Podcast ",
             "Developer Weekly Podcast",
             "The Stack Overflow Podcast",
             "The Hanselminutes Podcast",
             "The .NET Rocks Podcast",
             "The Azure Podcast",
             "The AWS Podcast",
             "The Rabbit Hole Podcast",
             "The .NET Core Podcast",
         };
    }

});

app.Run();

record Podcast(Guid Id, string Title);
