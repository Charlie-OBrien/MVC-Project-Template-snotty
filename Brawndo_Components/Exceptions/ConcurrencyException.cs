namespace Brawndo_Components.Exceptions
{
    /// <summary>
    /// Thrown when a row was changed or removed by someone else between read and write.
    /// Callers should re-read the record and let the user resolve the conflict.
    /// </summary>
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message)
        {
        }

        public ConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
