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
        private readonly string _connectionString;

        public AnimalsController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // GET: api/<AnimalsController>
        [HttpGet]
        public IActionResult Get(string orderBy = "Name")
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return BadRequest("Wrong param [orderBy].");
            }
            orderBy = orderBy.Trim().ToLower();
            if (orderBy != "name" && orderBy != "description" && orderBy != "category" && orderBy != "area")
            {
                return BadRequest("Wrong param [orderBy].");
            }
            using var con = new SqlConnection(_connectionString);
            con.Open();
            using var com = new SqlCommand($"SELECT * FROM Animal ORDER BY {orderBy} ASC", con);
            //com.Parameters.Add("@orderBy", SqlDbType.NChar, 50).Value = orderBy;

            using var reader = com.ExecuteReader();

            // Code to use without class Animal
            /*var fieldCount = reader.FieldCount;
            var columnNames = new List<int>(fieldCount).Select(i => reader.GetName(i)).ToList();
            // https://learn.microsoft.com/en-us/dotnet/api/system.data.idatarecord?view=net-6.0
            var r = reader.Cast<IDataRecord>()
                .Select(entity => new List<int>(fieldCount).ToDictionary(i => columnNames[i], i => entity[i])).ToList();*/

            // Code to use with Animal class
            var r = reader.Cast<IDataRecord>()
                .Select(r => Animal.Convert(r["IdAnimal"], r["Name"], r["Description"], r["Category"], r["Area"])).ToList();

            return Ok(r);
        }

        // Dodatkowo napisana z rozpedu, ale zostawiam, aby były wszystkie metody REST.
        // GET api/<AnimalsController>/5
        [HttpGet("{idAnimal:int}")]
        public IActionResult Get(int idAnimal)
        {
            if (idAnimal < 0)
            {
                return BadRequest("Wrong param [idAnimal].");
            }

            using var con = new SqlConnection(_connectionString);
            var animal = GetAnimal(idAnimal, con);
            if (animal == null)
            {
                return NotFound(idAnimal);
            }

            return Ok(animal);
        }


        // POST api/<AnimalsController>
        [HttpPost]
        public IActionResult Post([FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (animal.IdAnimal != 0)
            {
                return BadRequest("[Id] should be empty.");
            }
            using var con = new SqlConnection(_connectionString);
            using var com = new SqlCommand("INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)", con);
            com.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = animal.Name;
            com.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = animal.Description;
            com.Parameters.Add("@Category", SqlDbType.NVarChar, 200).Value = animal.Category;
            com.Parameters.Add("@Area", SqlDbType.NVarChar, 200).Value = animal.Area;

            con.Open();
            var r = com.ExecuteNonQuery();
            if (r < 0)
            {
                return Problem("Database returned a negative value: " + r);
            }

            return Created("api/Animals", animal);
        }

        // PUT api/<AnimalsController>/5
        [HttpPut("{idAnimal:int}")]
        public IActionResult Put(int idAnimal, [FromBody] Animal animal)
        {
            if (idAnimal < 0)
            {
                return BadRequest("Wrong param [idAnimal].");
            }

            using var con = new SqlConnection(_connectionString);
            var dbAnimal = GetAnimal(idAnimal, con);
            if (dbAnimal == null)
            {
                return NotFound(idAnimal);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (animal.IdAnimal != idAnimal)
            {
                return BadRequest("[Id] should be the same as in the URL.");
            }

            using var com = new SqlCommand("UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal", con);
            com.Parameters.Add("@IdAnimal", SqlDbType.Int).Value = idAnimal;
            com.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = animal.Name;
            com.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = animal.Description;
            com.Parameters.Add("@Category", SqlDbType.NVarChar, 200).Value = animal.Category;
            com.Parameters.Add("@Area", SqlDbType.NVarChar, 200).Value = animal.Area;

            var r = com.ExecuteNonQuery();
            if (r < 0)
            {
                return Problem("Database returned an error: " + r);
            }

            return Accepted(animal);
        }

        // DELETE api/<AnimalsController>/5
        [HttpDelete("{idAnimal}")]
        public IActionResult Delete(int idAnimal)
        {
            if (idAnimal < 0)
            {
                return BadRequest("Wrong param [idAnimal].");
            }

            using var con = new SqlConnection(_connectionString);
            var dbAnimal = GetAnimal(idAnimal, con);
            if (dbAnimal == null)
            {
                return NotFound(idAnimal);
            }
            var com = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @IdAnimal", con);
            com.Parameters.Add("@IdAnimal", SqlDbType.Int).Value = idAnimal;

            var r = com.ExecuteNonQuery();
            if (r < 0)
            {
                return Problem("Database returned an error: " + r);
            }

            return Ok();
        }

        /// <summary>
        /// Uses created connection to get animal from database.
        /// </summary>
        /// <param name="idAnimal"></param>
        /// <param name="con"></param>
        /// <returns>object if found, null otherwise</returns>
        private Animal? GetAnimal(int idAnimal, SqlConnection con)
        {
            using var com = new SqlCommand("SELECT * FROM Animal WHERE IdAnimal = @IdAnimal", con);
            com.Parameters.Add("@IdAnimal", SqlDbType.Int).Value = idAnimal;

            con.Open();
            using var reader = com.ExecuteReader();
            if (!reader.Read())
            {
                {
                    return null;
                }
            }
            var animal = Animal.Convert(reader["IdAnimal"], reader["Name"], reader["Description"], reader["Category"],
                reader["Area"]);

            return animal;
        }
    }
}
