using Npgsql;
using System.Data.Common;

namespace ConsoleApp1.Controller
{
    public class PostgresController
    {
       public const string ConnectionString = "User ID=postgres; Password=sa;" +
       "Host=localHost; Port=5432; Database=postgres; Pooling=True;";
        public void GetDbInfo()
        {
            using(DbConnection connection = new NpgsqlConnection(ConnectionString)) 
            {
                string query = "SELECT * FROM Superheroes " +
                    "\r\nWHERE ID NOT IN(4710, 3670, 3535)" +
                    "\r\nORDER BY NAME" +
                    "\r\nLIMIT(10)";

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
}
