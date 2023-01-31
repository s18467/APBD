using Microsoft.AspNetCore.Mvc;
using StudentVisor.Models;
using StudentVisor.Repositories;
using System.Text.RegularExpressions;

namespace StudentVisor.Controllers
{
    /// <summary>
    /// Controller for Students with API CRUD operations
    /// base route: StudentsController = api/students
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public IRepository<Student> StudentRepository { get; }

        public StudentsController(IRepository<Student> studentRepository)
        {
            StudentRepository = studentRepository;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        // GET: api/<StudentsController>
        [HttpGet]
        public IActionResult Get()
        {
            var list = StudentRepository.GetAll();
            return Ok(list.Select(StudentViewModel.ToViewModel));
        }

        /// <summary>
        /// Get student by index
        /// </summary>
        /// <param name="index">index of student in format sXXXX+</param>
        /// <returns></returns>
        // GET api/<StudentsController>/s1234
        [HttpGet("{index}")]
        public IActionResult Get(string index)
        {
            if (string.IsNullOrEmpty(index))
            {
                return BadRequest("Index should not be empty");
            }
            if (!Regex.IsMatch(index, @"^s[0-9]{1,8}$"))
            {
                return BadRequest("Index should be in format sXXXX");
            }
            var student = StudentRepository.GetById(index);
            if (student == null)
            {
                return NotFound($"Student with index {index} not found");
            }
            return Ok(StudentViewModel.ToViewModel(student));
        }

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var s = StudentViewModel.ToModel(student);
            if (!Student.Validate(s))
            {
                return BadRequest("Student is not valid");
            }
            if (StudentRepository.TryAdd(s))
            {
                return Created("Student created", s);
            }
            return Conflict("That Student already exists");
        }

        /// <summary>
        /// Updates existing student
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newStudent"></param>
        // PUT api/<StudentsController>/s5232
        [HttpPut("{index}")]
        public IActionResult Put(string index, [FromBody] StudentViewModel newStudent)
        {
            if (!index.Equals(newStudent.Index))
            {
                return BadRequest("Index mismatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (StudentRepository.GetById(index) == null)
            {
                return NotFound($"Student with index: {index} not found");
            }
            var s = StudentRepository.Update(StudentViewModel.ToModel(newStudent));
            return Accepted(StudentViewModel.ToViewModel(s));
        }

        /// <summary>
        /// Removes student from db by index
        /// </summary>
        /// <param name="index"></param>
        // DELETE api/<StudentsController>/5
        [HttpDelete("{index}")]
        public IActionResult Delete(string index)
        {

            if (StudentRepository.TryDelete(index))
            {
                return Ok();
            }
            return NotFound($"Student with index: {index} not found");
        }
    }
}
