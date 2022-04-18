using AutoMapper;
using DFMS.Database.Models;
using DFMS.Shared.Dto;

namespace DFMS.WebApi.Mapping
{
    public class FormCreatorMapping : Profile
    {
        public FormCreatorMapping()
        {
            //CreateMap<DbFormFieldDefinition, FormFieldDefinition>();
            //CreateMap<DbFormPredefiniedField, FormFieldDefinition>()
            //    .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.BaseDefinition.Type))
            //    .ForMember(dest => dest.ValueTypes, opts => opts.MapFrom(src => new[] { src.ValueType.Code }));

            //CreateMap<DbFormFieldValidationRuleDefinition, FormFieldValidationRuleDefinition>()
            //    .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.ValidationType.Code))
            //    .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.DefaultMessage))
            //    .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.DefaultValue));
        }
    }
}
