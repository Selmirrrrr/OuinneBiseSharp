namespace Bizy.OuinneBiseSharp.Exceptions
{
    using System;

    internal class OuinneBiseSharpException : Exception
    {
        public OuinneBiseSharpException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
