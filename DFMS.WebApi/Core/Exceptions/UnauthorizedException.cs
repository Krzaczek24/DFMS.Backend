using Core.WebApi.Exceptions.HttpErrorExceptions;
using DFMS.WebApi.Core.Errors;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Core.Exceptions
{
    public class UnauthorizedException : UnauthorizedException<ErrorModel>
    {
        public UnauthorizedException(Exception? innerException = null)
            : this(ErrorCode.Unauthorized, innerException) { }

        public UnauthorizedException(ErrorModel error, Exception? innerException = null)
            : base(error, innerException) { }

        public UnauthorizedException(IEnumerable<ErrorModel> errors, Exception? innerException = null, string message = "")
            : base(errors, innerException, message) { }

        public UnauthorizedException(ErrorCode error, Exception? innerException = null)
            : base(new ErrorModel(error), innerException) { }
    }
}
