using System;

namespace AoCTools.Error.Exception
{
    public class UnknownEnumValueException : System.Exception
    {
        public UnknownEnumValueException(Enum enumValue, Type enumType, System.Exception innerException = null)
            : base($"Unknown enum value '{enumValue}' for enum type '{enumType}'.", innerException)
        { }
    }
}