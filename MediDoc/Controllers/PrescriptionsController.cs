using MediDoc.Dtos;
using MediDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediDoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly MediContext _context;

        public PrescriptionsController(MediContext context)
        {
            _context = context;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDto>> GetPrescription(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.IdPatientNavigation)
                .Include(p => p.IdDoctorNavigation)
                .Include(p => p.PrescriptionMedicaments)
                .ThenInclude(pm => pm.IdMedicamentNavigation)
                .SingleOrDefaultAsync(p => p.IdPrescription == id);

            if (prescription == null)
            {
                return NotFound();
            }
            var patient = prescription.IdPatientNavigation;
            var doctor = prescription.IdDoctorNavigation;
            var prescriptionMedicaments = prescription.PrescriptionMedicaments;

            var prescriptionDto = new PrescriptionDto
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                Patient = new PatientDto
                {
                    IdPatient = patient.IdPatient,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Birthdate = patient.Birthdate
                },
                Doctor = new DoctorDto
                {
                    IdDoctor = doctor.IdDoctor,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email
                },
                Medicaments = prescriptionMedicaments
                    .Select(pm => new MedicamentDto
                    {
                        IdMedicament = pm.IdMedicamentNavigation.IdMedicament,
                        Name = pm.IdMedicamentNavigation.Name,
                        Description = pm.IdMedicamentNavigation.Description,
                        Type = pm.IdMedicamentNavigation.Type
                    })
                    .ToList()
            };

            return prescriptionDto;
        }
    }
}
