namespace KironBackendProject.Data.Dtos
{

    public class BankHolidayResponse
    {
        public Dictionary<string, RegionHolidays> Divisions { get; set; }
    }

    public class RegionHolidays
    {
        public string Division { get; set; }
        public List<HolidayEvent> Events { get; set; }
    }

    public class HolidayEvent
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool Bunting { get; set; }
    }


}
