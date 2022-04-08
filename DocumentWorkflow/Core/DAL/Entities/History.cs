namespace DocumentWorkflow.Core.DAL.Entities
{
    public class History
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string EditedField { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
