using Core.WebApi.Exceptions.HttpErrorExceptions;
using DFMS.WebApi.Core.Errors;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Core.Exceptions
{
    public class NotFoundException : NotFoundException<ErrorModel>
    {
        public NotFoundException(Exception? innerException = null)
            : base(innerException) { }

        public NotFoundException(ErrorModel error, Exception? innerException = null)
            : base(error, innerException) { }

        public NotFoundException(IEnumerable<ErrorModel> errors, Exception? innerException = null, string message = "")
            : base(errors, innerException, message) { }

        public NotFoundException(ErrorCode error, Exception? innerException = null)
            : base(new ErrorModel(error), innerException) { }
    }
}
