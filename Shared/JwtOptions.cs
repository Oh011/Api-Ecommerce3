namespace Shared
{
    public class JwtOptions
    {

        public string Issuer { get; set; }


        public string Audiance { get; set; }


        public string SecretKey { get; set; }


        public double ExpirationInDays { get; set; }
    }
}
