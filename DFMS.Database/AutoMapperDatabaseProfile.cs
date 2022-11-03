using AutoMapper;
using DFMS.Database.Dto;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.Database
{
    public class AutoMapperDatabaseProfile : Profile
    {
        public AutoMapperDatabaseProfile()
        {
            CreateMap<DbFormFieldDefinition, FormFieldDefinition>();
            CreateMap<DbFormPredefiniedField, FormFieldDefinition>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.BaseDefinition.Type))
                .ForMember(dest => dest.ValueTypes, opts => opts.MapFrom(src => new[] { src.ValueType.Code }));

            CreateMap<DbFormFieldValidationRuleDefinition, FormFieldValidationRuleDefinition>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.ValidationType.Code))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.DefaultMessage))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.DefaultValue));

            CreateMap<IEnumerable<UserRow>, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.First().Id))
                .ForMember(dest => dest.Login, opts => opts.MapFrom(src => src.First().Login))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.First().Role))
                .ForMember(dest => dest.Permissions, opts => opts.MapFrom(src => src.Select(s => s.Permission).Where(s => s != null).ToArray()))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.First().FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.First().LastName));
        }
    }
}
