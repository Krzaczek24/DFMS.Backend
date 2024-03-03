using Core.WebApi.Interfaces;
using KrzaqTools.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DFMS.WebApi.Common.Errors
{
    public class ErrorModel : IError
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorCode Code { get; set; }
        public string Message { get; set; } = string.Empty;

        public ErrorModel() { }

        public ErrorModel(ErrorCode code)
        {
            Code = code;
            Message = code.GetDescription() ?? string.Empty;
        }
    }
}
