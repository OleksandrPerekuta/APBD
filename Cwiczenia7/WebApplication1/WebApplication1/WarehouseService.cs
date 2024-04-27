using System.Data;
using System.Data.SqlClient;

namespace WebApplication1;

public class WarehouseService
{
    private readonly string _connectionString;

    public WarehouseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int AddProductToWarehouse(int idProduct, int idWarehouse, int amount, DateTime createdAt)
    {
        int newId = 0;

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                if (!ProductExists(connection, idProduct))
                {
                    throw new Exception("Invalid parameter: Provided IdProduct does not exist");
                }

                if (!WarehouseExists(connection, idWarehouse))
                {
                    throw new Exception("Invalid parameter: Provided IdWarehouse does not exist");
                }

                if (amount <= 0.2)
                {
                    throw new Exception("Invalid parameter: Amount should be greater than 0.2");
                }

                int orderId = GetOrderIdForProduct(connection, idProduct, amount, createdAt);
                if (orderId == 0)
                {
                    throw new Exception("Invalid parameter: There is no order to fulfill");
                }

                if (OrderFulfilled(connection, orderId))
                {
                    throw new Exception("Invalid parameter: The order has already been fulfilled");
                }

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddProductToWarehouse";

                command.Parameters.AddWithValue("@IdProduct", idProduct);
                command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);

                SqlParameter newIdParameter = new SqlParameter("@NewId", SqlDbType.Int);
                newIdParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(newIdParameter);

                command.ExecuteNonQuery();

                newId = Convert.ToInt32(newIdParameter.Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        return newId;
    }

    private bool ProductExists(SqlConnection connection, int idProduct)
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = "select count(*) from Product where IdProduct = @IdProduct";
            command.Parameters.AddWithValue("@IdProduct", idProduct);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }

    private bool WarehouseExists(SqlConnection connection, int idWarehouse)
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = "select count(*) from Warehouse where IdWarehouse = @IdWarehouse";
            command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        } 
    }

    private int GetOrderIdForProduct(SqlConnection connection, int idProduct, int amount, DateTime createdAt)
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @"select top 1 o.IdOrder 
                                from [Order] o
                                left join Product_Warehouse pw n o.IdOrder = pw.IdOrder
                                where o.IdProduct = @IdProduct
                                and o.Amount = @Amount
                                and o.CreatedAt < @CreatedAt
                                and pw.IdProductWarehouse is null";
            command.Parameters.AddWithValue("@IdProduct", idProduct);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@CreatedAt", createdAt);
            object result = command.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        } 
    }

    private bool OrderFulfilled(SqlConnection connection, int orderId)
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = "select cound(*) from [Order] where IdOrder = @OrderId and FulfilledAt is not null";
            command.Parameters.AddWithValue("@OrderId", orderId);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        } 
    }
}