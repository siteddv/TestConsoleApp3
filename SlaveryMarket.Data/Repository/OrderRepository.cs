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
            decimal totalPrice = 0;
            
            foreach (var orderItemIntentDto in orderIntentDto.OrderItems)
            {
                using DbCommand productPriceCommand = connection.CreateCommand();
                productPriceCommand.CommandText = 
                    "SELECT price FROM product WHERE id = @product_id";
                productPriceCommand.Parameters.Add(
                    new NpgsqlParameter("product_id", orderItemIntentDto.ProductId));
                totalPrice += (decimal)productPriceCommand.ExecuteScalar() * orderItemIntentDto.Amount;
            }
            
            using DbCommand moneyWithdrawingCommand = connection.CreateCommand();
            moneyWithdrawingCommand.CommandText = 
                "UPDATE client" +
                    " SET money_amount = money_amount - @total_price " +
                    " WHERE id = @client_id";

            moneyWithdrawingCommand.Parameters.Add(
                new NpgsqlParameter("total_price", totalPrice));
            moneyWithdrawingCommand.Parameters.Add(
                new NpgsqlParameter("client_id", orderIntentDto.BuyerId));

            moneyWithdrawingCommand.ExecuteNonQuery();

            using DbCommand orderInsertCommand = connection.CreateCommand();
            orderInsertCommand.CommandText = 
                """
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
                new NpgsqlParameter("order_status", (int)OrderStatus.InProgress));
            orderInsertCommand.Parameters.Add(
                new NpgsqlParameter("total_price", totalPrice));
            orderInsertCommand.Parameters.Add(
                new NpgsqlParameter("create_date", DateTime.Now));
            
            var orderId = (long)orderInsertCommand.ExecuteScalar();
            
            foreach (var orderItemIntentDto in orderIntentDto.OrderItems)
            {
                using DbCommand orderItemInsertCommand = connection.CreateCommand();
                orderItemInsertCommand.CommandText = 
                    """
                     INSERT INTO
                        order_item
                            (order_id, product_id, amount)
                        VALUES
                            (@order_id, @product_id, @amount)
                     """;
                orderItemInsertCommand.Parameters.Add(
                    new NpgsqlParameter("order_id", orderId));
                orderItemInsertCommand.Parameters.Add(
                    new NpgsqlParameter("product_id", orderItemIntentDto.ProductId));
                orderItemInsertCommand.Parameters.Add(
                    new NpgsqlParameter("amount", orderItemIntentDto.Amount));
                
                orderItemInsertCommand.ExecuteNonQuery();
            }
            
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }
    }
}