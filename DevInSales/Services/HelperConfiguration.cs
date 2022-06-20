namespace DEVinSalesTest.Services
{
    public class HelperConfiguration
    {
        public static IConfiguration config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }
    }
}
