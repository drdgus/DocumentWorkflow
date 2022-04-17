namespace DocumentWorkflow.Core.DAL.Entities
{
    public class LogBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime NumberingResetDate { get; set; }
        public DateTime LastNumberingResetDate { get; set; }
    }
}
