namespace KironBackendProject.Data.Entities
{
    public class BankHoliday
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool Bunting { get; set; }
        public ICollection<RegionBankHoliday> RegionBankHolidays { get; set; }
    }
}
