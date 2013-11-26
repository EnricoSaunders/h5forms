using System;

namespace H5Forms.Dtos.Common
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
            
        }
    }
}
