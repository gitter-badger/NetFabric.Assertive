using System;

namespace NetFabric.Assertive
{
    public class AssertionException
        : Exception
    {
        public AssertionException(string message)
            : base(message)
        {
        }

        public AssertionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}