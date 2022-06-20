using DEVinSalesTest.Models;

namespace DEVinSalesTest.Seeds
{
    public class ProfileSeed
    {
        public static List<Profile> Seed { get; set; } = new List<Profile>() { new Profile(1, "Cliente") };
    }
}
