using Core.WebApi.Exceptions.HttpErrorExceptions;
using DFMS.WebApi.Common.Errors;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Common.Exceptions
{
    public class BadRequestException : BadRequestException<ErrorModel>
    {
        public BadRequestException(Exception? innerException = null)
            : base(innerException) { }

        public BadRequestException(ErrorModel error, Exception? innerException = null)
            : base(error, innerException) { }

        public BadRequestException(IEnumerable<ErrorModel> errors, Exception? innerException = null, string message = "")
            : base(errors, innerException, message) { }

        public BadRequestException(ErrorCode error, Exception? innerException = null)
            : base(new ErrorModel(error), innerException) { }
    }
}
