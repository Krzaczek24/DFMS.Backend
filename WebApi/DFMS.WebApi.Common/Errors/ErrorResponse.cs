using Core.WebApi.Responses;
using System.Collections.Generic;

namespace DFMS.WebApi.Common.Errors
{
    public class ErrorResponse : ErrorResponse<ErrorModel>
    {
        public ErrorResponse() { }

        public ErrorResponse(ErrorModel error) : base(error) { }

        public ErrorResponse(ICollection<ErrorModel> errors) : base(errors) { }
    }
}
