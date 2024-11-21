namespace TwistingNether.DataAccess.TwistingNether.Exceptions
{
    public class TokenRetrievalException : Exception
    {
        public TokenRetrievalException(string message) : base(message) { }
        public TokenRetrievalException(string message, Exception innerException) : base(message, innerException) { }
    }

}
