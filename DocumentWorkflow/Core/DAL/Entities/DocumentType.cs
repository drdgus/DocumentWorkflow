namespace DocumentWorkflow.Core.DAL.Entities
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DocumentCategoryId { get; set; }
    }
}
