using AutoMapper;
using MediatR;
using Patient.Application.Abstractions.Persistence;
using Patient.Application.DTOs;
using Patient.Application.Exceptions;
using Patient.Application.Handlers.Queries.GetPatient;

namespace Booking.Application.Handlers.Booking.Queries.GetBooking
{
    public class GetPatientQueryHandler :IRequestHandler<GetPatientQuery, PatientDto>
    {
        private readonly IBaseRepository<Patient.Domain.Patient> _baseRepository;
        private readonly IMapper _mapper;

        public GetPatientQueryHandler(IBaseRepository<Patient.Domain.Patient> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.PatientId);
            var patient = await _baseRepository.SingleOrDefaultAsync(i => i.Id == idGuid && i.Active, cancellationToken);
            if (patient == null)
            {
                throw new NotFoundException($"Patient with id = {request.PatientId} not found");
            }

            return _mapper.Map<PatientDto>(patient);
        }
    }
}
