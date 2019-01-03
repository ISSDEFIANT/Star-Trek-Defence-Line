using System;

namespace Saving
{
    public sealed class SaveFormatException : Exception
    {
        public SaveFormatException(string message) : base(message)
        {
        }
    }
}