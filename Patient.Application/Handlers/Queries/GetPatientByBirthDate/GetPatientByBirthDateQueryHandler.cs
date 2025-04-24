using AutoMapper;
using MediatR;
using Patient.Application.Abstractions.Persistence;
using Patient.Application.DTOs;
using Patient.Application.Exceptions;

namespace Patient.Application.Handlers.Queries.GetPatientByBirthDate
{
    public class GetPatientByBirthDateQueryHandler : IRequestHandler<GetPatientByBirthDateQuery, BaseListDto<PatientByBirthDateDto>>
    {
        private readonly IBaseRepository<Domain.Patient> _baseRepository;
        private readonly IMapper _mapper;

        public GetPatientByBirthDateQueryHandler(IBaseRepository<Domain.Patient> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<BaseListDto<PatientByBirthDateDto>> Handle(GetPatientByBirthDateQuery request, CancellationToken cancellationToken)
        {
            DateTime startDate;
            DateTime endDate;

            // Определяем префикс и парсим дату
            var prefix = request.BirthDate.Substring(0, 2);
            var date = DateTime.Parse(request.BirthDate.Substring(2));

            switch (prefix)
            {
                case "ge": // Greater than or equal
                    startDate = date;
                    endDate = startDate.AddDays(1); // Включаем весь день
                    break;
                case "le": // Less than or equal
                    endDate = date.AddDays(1); // Включаем весь день
                    startDate = DateTime.MinValue; // Начинаем с самого раннего времени
                    break;
                case "lt": // Less than
                    endDate = date; // Не включаем этот день
                    startDate = DateTime.MinValue; // Начинаем с самого раннего времени
                    break;
                case "gt": // Greater than
                    startDate = date.AddDays(1); // Начинаем с следующего дня
                    endDate = DateTime.MaxValue; // До самого позднего времени
                    break;
                default:
                    throw new BadOperationException("Invalid date prefix. Allowed prefixes: ge - greater than or equal;" +
                        "le -less than or equal; lt - less than; gt - greater than");
            }

            var patients = await _baseRepository.ToArrayAsync(
                p => p.BirthDate >= startDate && p.BirthDate < endDate,
                cancellationToken
            );

            if (patients is null)
            {
                throw new NotFoundException($"There are no patients with a birth date between {request.BirthDate} and {endDate}.");
            }

            var items = _mapper.Map<PatientByBirthDateDto[]>(patients);
            return new BaseListDto<PatientByBirthDateDto>
            {
                Items = items,
                TotalCount = items.Length
            };
        }
    }
}
