namespace DocumentWorkflow.Core.DAL.Entities
{
    public class DocumentCategory
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
