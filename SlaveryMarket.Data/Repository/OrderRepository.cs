using System.Data.Common;
using Npgsql;
using SlaveryMarket.Data.Dto;

namespace SlaveryMarket.Data.Repository;

public class OrderRepository
{
    public void BuyProduct(OrderIntentDto orderIntentDto)
    {
        using DbConnection connection = new NpgsqlConnection(DefaultConfiguration.ConnectionString);
        connection.Open();
        using DbTransaction transaction = connection.BeginTransaction();
        try
        {
            using DbCommand moneyWithdrawingCommand = connection.CreateCommand();
            moneyWithdrawingCommand.CommandText = 
                "UPDATE client" +
                    " SET money_amount = money_amount - @total_price " +
                    " WHERE id = @client_id";

            moneyWithdrawingCommand.Parameters.Add(
                new NpgsqlParameter("total_price", orderIntentDto.TotalPrice));
            moneyWithdrawingCommand.Parameters.Add(
                new NpgsqlParameter("client_id", orderIntentDto.BuyerId));
            
            moneyWithdrawingCommand.ExecuteNonQuery();
        }
        catch
        {
            transaction.Rollback();
        }
    }
}