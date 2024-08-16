namespace WebApplication1.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application()
            {
                ApplicationId = 1,
                ApplicationName = "WebApp",
                ClientId = "A1b2C-D3e4F-G5h6I-J7k8L-M9n0O",
                Secret = "X7y8Z-W9a0B-C1d2E-F3g4H-I5j6K",
                Scopes = "read,write"
            }
        };

        public static Application? GetApplicationByClientId(string clientId) 
        {
            return _applications.FirstOrDefault(x=> x.ClientId == clientId);
        }
    }
}
