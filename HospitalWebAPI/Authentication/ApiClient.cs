namespace HospitalWebAPI.Authentication
{
    public class ApiClient : HttpClient
    {
        public ApiClient() : base()
        {
            DefaultRequestHeaders.Add("XApiKey", "WelCome123");
        }
    }

}
