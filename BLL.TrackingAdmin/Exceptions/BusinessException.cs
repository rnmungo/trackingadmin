namespace BLL.TrackingAdmin.Exceptions
{
    public class BusinessException : Exception
    {
        private BusinessCodeEnum _businessCode { get; set; }

        public BusinessCodeEnum BusinessCode { get => _businessCode; }

        public BusinessException(BusinessCodeEnum businessCode) : base()
        {
            _businessCode = businessCode;
        }

        public BusinessException(BusinessCodeEnum businessCode, string message) : base(message)
        {
            _businessCode = businessCode;
        }

        public BusinessException(BusinessCodeEnum businessCode, string message, Exception innerException) : base(message, innerException)
        {
            _businessCode = businessCode;
        }
    }
}
