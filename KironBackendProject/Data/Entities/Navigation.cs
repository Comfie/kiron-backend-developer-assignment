namespace KironBackendProject.Data.Entities
{
    public class Navigation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ParentId { get; set; }
    }
}
