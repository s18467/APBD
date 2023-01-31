using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using Warehouse.ViewModels;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly string _connectionString;
        private const string SqlProductExists = "Select Price FROM Product WHERE IdProduct = @IdProduct";
        private const string SqlWarehouseExists = "Select 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        private const string SqlOrderGetFulllfilled = "SELECT IdOrder, FulfilledAt FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt";
        private const string SqlProductWareExists = "SELECT 1 FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        private const string SqlUpdateOrderFulfilled = "UPDATE [Order] SET FulfilledAt = GETDATE() WHERE @IdOrder = IdOrder";
        private const string SqlProdWareAdd = "INSERT INTO Product_Warehouse (IdProduct, IdWarehouse, IdOrder, Price, Amount, CreatedAt) OUTPUT INSERTED.IdProductWarehouse VALUES (@IdProduct, @IdWarehouse, @IdOrder, @Price, @Amount, GETDATE())";

        public WarehousesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // POST api/<AnimalsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderPostViewModel orderPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await using var con = new SqlConnection(_connectionString);
            if (con.State == ConnectionState.Closed)
            {
                await con.OpenAsync();
            }

            if (await con.BeginTransactionAsync() is not SqlTransaction tran)
            {
                return Problem("Can't create transaction", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            await using var com = new SqlCommand(SqlProductExists, con, tran);
            com.Parameters.Add("@IdProduct", SqlDbType.Int).Value = orderPostViewModel.IdProduct;
            com.Parameters.Add("@IdWarehouse", SqlDbType.Int).Value = orderPostViewModel.IdWarehouse;
            com.Parameters.Add("@Amount", SqlDbType.Int).Value = orderPostViewModel.Amount;
            com.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = orderPostViewModel.CreatedAt;

            var dataReader = await com.ExecuteReaderAsync();
            if (!await dataReader.ReadAsync())
            {
                return NotFound($"Product with id {orderPostViewModel.IdProduct} not found");
            }

            com.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Precision = 25, Scale = 2 }).Value = dataReader["Price"];
            await dataReader.CloseAsync();

            com.CommandText = SqlWarehouseExists;
            dataReader = await com.ExecuteReaderAsync();
            if (!dataReader.HasRows)
            {
                return NotFound("Warehouse not found");
            }
            await dataReader.CloseAsync();

            com.CommandText = SqlOrderGetFulllfilled;
            dataReader = await com.ExecuteReaderAsync();
            if (!await dataReader.ReadAsync())
            {
                return NotFound("Order not found");
            }

            var orderId = dataReader["IdOrder"];
            var fullfiObj = dataReader["fulfilledAt"];
            await dataReader.CloseAsync();

            var t = fullfiObj?.GetType();
            if (t != typeof(DBNull))
            {
                return Conflict($"Order already fulfilled on {fullfiObj}");
            }

            com.Parameters.Add("@IdOrder", SqlDbType.Int).Value = orderId;

            com.CommandText = SqlProductWareExists;
            dataReader = await com.ExecuteReaderAsync();
            if (dataReader.Read())
            {
                return Conflict("Order already fulfilled");
            }
            await dataReader.CloseAsync();

            com.CommandText = SqlUpdateOrderFulfilled;
            var result = await com.ExecuteNonQueryAsync();
            if (result < 1)
            {
                return Problem("No Order rows affected.", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            com.CommandText = SqlProdWareAdd;
            result = (int)(await com.ExecuteScalarAsync() ?? -1);
            if (result < 1)
            {
                return Problem("No Product_Warehouse rows affected.", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            await tran.CommitAsync();
            return CreatedAtAction(nameof(Post), result);
        }
    }
}
