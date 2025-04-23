namespace Domain.Exceptions
{
    sealed public class UnauthorizedException : Exception
    {



        public UnauthorizedException(string msg = "Invalid email or password") : base(msg) { }

    }
}
