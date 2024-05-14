using Npgsql;
using System.Data.Common;

namespace ConsoleApp1.Controller
{
    public class PostgresController
    {
       public const string ConnectionString = "User ID=new_superuser; Password=postgres;" +
       "Host=localHost; Port=5432; Database=learn; Pooling=True;";
        public void GetDbInfo()
        {
            using DbConnection connection = new NpgsqlConnection(ConnectionString);
            string query = "SELECT name, align, universe FROM superheroes WHERE appearance_count > 100";

            connection.Open();

            using (DbCommand command = connection.CreateCommand()) 
            { 
                command.CommandText = query;
                using(DbDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        for(int i = 1; i< reader.FieldCount; i++)
                            Console.WriteLine($"{reader[i]}");

                        Console.WriteLine() ;
                    }

                }
            }
        }
    }
}
