namespace KironBackendProject.Data.Entities
{
    public class RegionBankHoliday
    {
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public int BankHolidayId { get; set; }
        public BankHoliday BankHoliday { get; set; }
    }
}
