using System.Data.Common;
using Npgsql;
using SlaveryMarket.Data.Dto;
using SlaveryMarket.Data.Enums;

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

            using DbCommand orderInsertCommand = connection.CreateCommand();
            orderInsertCommand.CommandText = """
                                             INSERT INTO
                                                order_info
                                                    (buyer_id, order_status, total_price, create_date)
                                                VALUES
                                                    (@client_id, @order_status, @total_price, @create_date)
                                             RETURNING id
                                             """;
            orderInsertCommand.Parameters.Add(
                new NpgsqlParameter("client_id", orderIntentDto.BuyerId));
            orderInsertCommand.Parameters.Add(
                new NpgsqlParameter("order_status", OrderStatus.InProgress));
            orderInsertCommand.Parameters.Add(
                new NpgsqlParameter("total_price", orderIntentDto.TotalPrice));
            orderInsertCommand.Parameters.Add(
                new NpgsqlParameter("create_date", DateTime.Now));
            
            
        }
        catch
        {
            
            transaction.Rollback();
        }
    }
}