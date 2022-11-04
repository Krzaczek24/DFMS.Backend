using System;

namespace DFMS.Database.Exceptions
{
    public class DuplicatedEntryException : Exception
    {
        public DuplicatedEntryException() : base() { }

        public DuplicatedEntryException(string message) : base(message) { }

        public DuplicatedEntryException(string message, Exception ex) : base(message, ex) { }
    }
}
