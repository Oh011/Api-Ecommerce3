namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {


        public UserNotFoundException(string email) : base($"The user with email {email} is not found")
        {



        }
    }
}
