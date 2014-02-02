using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMail
{
    public class MiniMailException : Exception
    {
        public MiniMailException() : base() { }
        public MiniMailException(string message) : base(message) { }
        public MiniMailException(string message, Exception innerException) : base(message, innerException) { }
    }
}
