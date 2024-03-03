using Core.WebApi.Exceptions.HttpErrorExceptions;
using DFMS.WebApi.Common.Errors;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Common.Exceptions
{
    public class ConflictException : ConflictException<ErrorModel>
    {
        public ConflictException(Exception? innerException = null)
            : base(innerException) { }

        public ConflictException(ErrorModel error, Exception? innerException = null)
            : base(error, innerException) { }

        public ConflictException(IEnumerable<ErrorModel> errors, Exception? innerException = null, string message = "")
            : base(errors, innerException, message) { }

        public ConflictException(ErrorCode error, Exception? innerException = null)
            : base(new ErrorModel(error), innerException) { }
    }
}
