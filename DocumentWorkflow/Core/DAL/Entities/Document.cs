namespace DocumentWorkflow.Core.DAL.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int DocumentCategoryId { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
    }
}
