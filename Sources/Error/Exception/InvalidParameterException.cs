namespace AoCTools.Error.Exception
{
    public class InvalidParameterException : System.Exception
    {
        public InvalidParameterException(string paramName, string message, System.Exception innerException = null)
            : base($"Invalid parameter {paramName}: {message}.", innerException)
        { }
    }
}