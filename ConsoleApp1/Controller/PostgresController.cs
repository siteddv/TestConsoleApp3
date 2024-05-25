using Npgsql;
using System.Data.Common;

namespace ConsoleApp1.Controller
{
    public class PostgresController
    {
       public const string ConnectionString = "User ID=admin; Password=admin;" +
       "Host=localHost; Port=5432; Database=learn; Pooling=True;";
       
       private const string NameColumn = "hero_name";
       private const string AlignColumn = "align";
       private const string UniverseColumn = "universe";
       
        public void GetDbInfo()
        {
            using DbConnection connection = new NpgsqlConnection(ConnectionString);
            string query = $"SELECT {UniverseColumn}, {AlignColumn}, {NameColumn} FROM superheroes WHERE appearance_count > 1";

            connection.Open();

            using (DbCommand command = connection.CreateCommand()) 
            { 
                command.CommandText = query;
                using(DbDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal(NameColumn));
                        string universe = reader.GetString(reader.GetOrdinal(UniverseColumn));
                        string align = reader.GetString(reader.GetOrdinal(AlignColumn));
                        Console.WriteLine($"Name: {name}, Universe: {universe}, Align: {align}");
                    }

                }
            }
        }
    }
}
