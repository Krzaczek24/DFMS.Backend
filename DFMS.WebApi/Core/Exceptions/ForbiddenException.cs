using Core.WebApi.Exceptions.HttpErrorExceptions;
using DFMS.WebApi.Core.Errors;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Core.Exceptions
{
    public class ForbiddenException : ForbiddenException<ErrorModel>
    {
        public ForbiddenException(Exception? innerException = null)
            : base(innerException) { }

        public ForbiddenException(ErrorModel error, Exception? innerException = null)
            : base(error, innerException) { }

        public ForbiddenException(IEnumerable<ErrorModel> errors, Exception? innerException = null, string message = "")
            : base(errors, innerException, message) { }

        public ForbiddenException(ErrorCode error, Exception? innerException = null)
            : base(new ErrorModel(error), innerException) { }
    }
}
