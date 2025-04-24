using AutoMapper;
using Patient.Application.DTOs;
using Patient.Application.Handlers.Commands.CreatePatient;
using Patient.Domain;

namespace Patient.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Маппинг из CreatePatientCommand в Patient
        CreateMap<CreatePatientCommand, Domain.Patient>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name
            {
                Id = Guid.NewGuid(),
                Use = src.Name.Use,
                Family = src.Name.Family,
                GivenNames = src.Name.GivenNames.Select(g => new GivenName
                {
                    Id = Guid.NewGuid(),
                    Value = g.Value
                }).ToList()
            }));

        // Маппинг из Patient в PatientDto
        CreateMap<Domain.Patient, PatientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameDto
            {
                Use = src.Name.Use,
                Family = src.Name.Family,
                GivenNames = src.Name.GivenNames.Select(g => new GivenNameDto
                {
                    Value = g.Value
                }).ToList()
            }));

        // Маппинг из Patient в PatientDto
        CreateMap<Domain.Patient, PatientByBirthDateDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameDto
            {
                Use = src.Name.Use,
                Family = src.Name.Family,
                GivenNames = src.Name.GivenNames.Select(g => new GivenNameDto
                {
                    Value = g.Value
                }).ToList()
            }));

        // Маппинг для Name и NameDto
        CreateMap<Name, NameDto>();
        CreateMap<NameDto, Name>();
    }
}
