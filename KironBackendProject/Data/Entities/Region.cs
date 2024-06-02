namespace KironBackendProject.Data.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RegionBankHoliday> RegionBankHolidays { get; set; }
    }
}
