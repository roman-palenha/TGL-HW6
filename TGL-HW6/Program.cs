using Dapper;
using System.Data.SqlClient;
using TGL_HW6;

class Program
{
    private static SqlConnection _connection;
    static async Task Main(string[] args)
    {
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=model;Trusted_Connection=True;MultipleActiveResultSets=true; User ID = test1; Password=123321";
        _connection = new SqlConnection(connectionString);
        await TestDapper();
        _connection.Dispose();
    }

    private static async Task TestDapper()
    {
        if(_connection.State != System.Data.ConnectionState.Open)
        {
            _connection.Open();
        }

        if(!(await IsTableCreatedAsync(DatabaseConstants.PuppiesTableName)))
        {
            await CreatePuppiesTableAsync();
        }

        await PopulatePuppiesTableWithRandomDataAsync();

        Console.WriteLine($"Current number of rows - { await GetPuppiesCountAsync()}");

        await DropPuppiesTable();
    }

    private async static Task<bool> IsTableCreatedAsync(string tableName)
    {
        var result = await _connection.QuerySingleAsync<bool>(Queries.CheckIfTableExistsQuery, new { TableName = tableName });
        return result;
    }

    private async static Task CreatePuppiesTableAsync()
    {
        var result = await _connection.ExecuteAsync(Queries.CreatePuppiesTableQuery);
        if (result != -1)
        {
            throw new ApplicationException("Failed to delete puppies table.");
        }
    }

    private async static Task PopulatePuppiesTableWithRandomDataAsync()
    {
        var puppies = new List<Puppies>()
        {
            new Puppies()
            {
                Name = "Balloon",
                BirthDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(432)),
                HeightInCm = new Random().Next(25, 80) + new Random().NextDouble(),
                LengthInCm = new Random().Next(40, 150) + new Random().NextDouble()
            },
            new Puppies()
            {
                Name = "Jack",
                BirthDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(432)),
                HeightInCm = new Random().Next(25, 80) + new Random().NextDouble(),
                LengthInCm = new Random().Next(40, 150) + new Random().NextDouble()
            },
            new Puppies()
            {
                Name = "Johny",
                BirthDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(432)),
                HeightInCm = new Random().Next(25, 80) + new Random().NextDouble(),
                LengthInCm = new Random().Next(40, 150) + new Random().NextDouble()
            },
            new Puppies()
            {
                Name = "Ricky",
                BirthDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(432)),
                HeightInCm = new Random().Next(25, 80) + new Random().NextDouble(),
                LengthInCm = new Random().Next(40, 150) + new Random().NextDouble()
            }
        };
       await _connection.ExecuteAsync(Queries.InsertPuppiesDataQuery, puppies);
    }

    private async static Task<int> GetPuppiesCountAsync()
    {
        var result = await _connection.QuerySingleAsync<int>(Queries.CountPuppiesTableQuery);
        return result;
    }

    private async static Task DropPuppiesTable()
    {
        var result = await _connection.ExecuteAsync(Queries.DeletePupiesTableQuery);
        if (result != -1)
        {
            throw new ApplicationException("Failed to delete puppies table.");
        }
    }
}