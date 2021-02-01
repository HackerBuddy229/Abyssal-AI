using System;

namespace AbyssalAI.Core.helpers
{
    public class InvalidDataWindowException : Exception
    {
        public InvalidDataWindowException(string msg) : base(msg){ }

        public InvalidDataWindowException() { }
    }
}