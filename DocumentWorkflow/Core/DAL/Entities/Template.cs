namespace DocumentWorkflow.Core.DAL.Entities
{
    public class Template
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Filename { get; set; }
    }
}
