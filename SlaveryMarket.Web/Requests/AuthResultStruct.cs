namespace SlaveryMarket.Web.Requests
{
    public struct AuthResultStruct
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }
    }
}
