using Core.WebApi.Exceptions.HttpErrorExceptions;
using DFMS.WebApi.Core.Errors;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Core.Exceptions
{
    public class InternalServerException : InternalServerException<ErrorModel>
    {
        public InternalServerException(Exception? innerException = null)
            : base(innerException) { }

        public InternalServerException(ErrorModel error, Exception? innerException = null)
            : base(error, innerException) { }

        public InternalServerException(IEnumerable<ErrorModel> errors, Exception? innerException = null, string message = "")
            : base(errors, innerException, message) { }

        public InternalServerException(ErrorCode error, Exception? innerException = null)
            : base(new ErrorModel(error), innerException) { }
    }
}
