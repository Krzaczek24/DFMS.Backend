using AutoMapper;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Dto.Users;
using DFMS.Database.Models;
using DFMS.Shared.Enums;
using System;

namespace DFMS.Database
{
    public class AutoMapperDatabaseProfile : Profile
    {
        public AutoMapperDatabaseProfile()
        {
            CreateMap<DbFormFieldDefinition, FormFieldDefinitionDto>();
            CreateMap<DbFormPredefiniedField, FormFieldDefinitionDto>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.BaseDefinition.Type))
                .ForMember(dest => dest.ValueTypes, opts => opts.MapFrom(src => new[] { src.ValueType.Code }));

            CreateMap<DbFormFieldValidationRuleDefinition, FormFieldValidationRuleDefinitionDto>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.ValidationType.Code))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.DefaultMessage))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.DefaultValue));

            CreateMap<DbUser, UserDto>()
                .ForMember(dest => dest.Permissions, opts => opts.MapFrom(src => new string[] { }))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => Enum.Parse<UserRole>(src.Role.Name, true)));
        }
    }
}
