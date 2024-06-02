namespace KironBackendProject.Shared
{
    public class AppSettings
    {
        public const string Section = "AppSettings";

        public string Secret { get; set; }
        public string BankHolidaysUrl { get; set; }
        public string CoinStatsUrl { get; set; }
    }
}
