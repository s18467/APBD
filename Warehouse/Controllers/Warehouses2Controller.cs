using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using Warehouse.ViewModels;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly string _connectionString;

        public Warehouses2Controller(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // POST api/<Warehouses2Controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderPostViewModel opViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await using var con = new SqlConnection(_connectionString);
            await using var com = new SqlCommand("AddProductToWarehouse", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@IdProduct", SqlDbType.Int).Value = opViewModel.IdProduct;
            com.Parameters.Add("@IdWarehouse", SqlDbType.Int).Value = opViewModel.IdWarehouse;
            com.Parameters.Add("@Amount", SqlDbType.Int).Value = opViewModel.Amount;
            com.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = opViewModel.CreatedAt;

            try
            {
                await con.OpenAsync();
                var r = await com.ExecuteScalarAsync();
                return r != null ? Ok(r) : Problem("Something strange happened.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
            catch (SqlException e)
            {
                return Problem(e.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
