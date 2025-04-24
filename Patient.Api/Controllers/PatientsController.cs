using Booking.Application.Handlers.Booking.Commands.UpdateBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patient.Application.DTOs;
using Patient.Application.Handlers.Commands.CreatePatient;
using Patient.Application.Handlers.Commands.DeletePatient;
using Patient.Application.Handlers.Commands.UpdatePatient;
using Patient.Application.Handlers.Queries.GetPatient;
using Patient.Application.Handlers.Queries.GetPatientByBirthDate;

namespace Patient.Api.Controllers
{
    /// <summary>
    /// Controller for managing patients.
    /// Provides methods for creating, updating, deleting, and getting information about patients.
    /// </summary>
    [Route("Patient")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        #region Commands
        /// <summary>
        /// Create new patient
        /// </summary>
        /// <param name="createPatientCommand">Command to create patient</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="mediator">Mediator for processing commands</param>
        /// <returns>Created patient</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync(
            CreatePatientCommand createPatientCommand,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var newPatient = await mediator.Send(createPatientCommand, cancellationToken);
            return Created("/Patient/" + newPatient.Name.Use, newPatient);
        }

        /// <summary>
        /// Delete patient by ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="mediator">Mediator for processing commands</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Operation execution status</returns>
        [HttpDelete("{patientId}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] string patientId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(new DeletePatientCommand() { Id = patientId }, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Update patient information by ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="payload">Patient update data</param>
        /// <param name="mediator">Mediator for processing commands</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated patient</returns>
        [HttpPut("{patientId}")]
        public async Task<PatientDto> UpdateAsync([FromRoute] string patientId, [FromBody] UpdatePatientPayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdatePatientCommand
            {
                patientId = patientId,
                Gender = payload.Gender,
                BirthDate = payload.BirthDate,
                Active = payload.Active,
                Name = payload.Name,
            }, cancellationToken);
        }
        #endregion

        #region Queries
        /// <summary>
        /// Get patient by ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="mediator">Mediator for handling requests</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Patient with the specified identifier</returns>
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetByIdAsync(
            string patientId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var patient = await mediator.Send(new GetPatientQuery() { PatientId = patientId }, cancellationToken);
            return Ok(patient);
        }

        /// <summary>
        /// Search for patients by date of birth
        /// </summary>
        /// <param name="birthDate">Start date</param>
        /// <param name="mediator">Mediator for processing requests</param>
        /// <returns>List of patients</returns>
        [HttpGet("BirthDate")]
        public async Task<IActionResult> GetByBirthDateAsync([FromQuery] string birthDate, IMediator mediator)
        {
            var patients = await mediator.Send(new GetPatientByBirthDateQuery() { BirthDate = birthDate });
            return Ok(patients);
        }
        #endregion
    }
}
