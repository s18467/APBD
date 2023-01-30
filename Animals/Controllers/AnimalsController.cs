using Animals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Animals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public AnimalsController(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        // GET: api/<AnimalsController>
        [HttpGet]
        public IActionResult Get()
        {
            using var con = new SqlConnection(_connectionString);
            con.Open();
            using var com = new SqlCommand("SELECT * FROM Animal", con);
            using var reader = com.ExecuteReader();

            // Code to use without class Animal
            /*var fieldCount = reader.FieldCount;
            var columnNames = new List<int>(fieldCount).Select(i => reader.GetName(i)).ToList();
            // https://learn.microsoft.com/en-us/dotnet/api/system.data.idatarecord?view=net-6.0
            var r = reader.Cast<IDataRecord>()
                .Select(entity => new List<int>(fieldCount).ToDictionary(i => columnNames[i], i => entity[i])).ToList();*/

            // Code to use with Animal class
            var r = reader.Cast<IDataRecord>()
                .Select(record => new Animal
                {
                    IdAnimal = Convert.ToInt32(record["IdAnimal"]),
                    Name = Convert.ToString(record["Name"]),
                    Description = Convert.ToString(record["Description"]),
                    Category = Convert.ToString(record["Category"]),
                    Area = Convert.ToString(record["Area"])
                })
                .ToList();

            return Ok(r);
        }

        // GET api/<AnimalsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AnimalsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AnimalsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AnimalsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
