namespace DAL.TrackingAdmin.Exceptions
{
    public class EFCoreDataException : Exception
    {
        public EFCoreDataException() : base()
        {
        }

        public EFCoreDataException(string message) : base(message)
        {
        }

        public EFCoreDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
