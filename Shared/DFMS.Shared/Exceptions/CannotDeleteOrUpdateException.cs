﻿using System;

namespace DFMS.Shared.Exceptions
{
    public class CannotDeleteOrUpdateException : Exception
    {
        public CannotDeleteOrUpdateException() : base() { }

        public CannotDeleteOrUpdateException(string message) : base(message) { }

        public CannotDeleteOrUpdateException(string message, Exception ex) : base(message, ex) { }
    }
}
