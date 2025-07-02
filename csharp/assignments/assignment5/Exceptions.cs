using System;

namespace assignment5
{
    class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    class ScholarshipNotApplicableException : ApplicationException
    {
        public ScholarshipNotApplicableException(string message) : base(message) { }
    }
}
