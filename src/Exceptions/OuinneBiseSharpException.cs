namespace Bizy.OuinneBiseSharp.Exceptions
{
    using System;

    internal class OuinneBiseSharpException : Exception
    {
        public OuinneBiseSharpException(string message, Exception e) : base(message, e)
        {
            
        }
    }
}
