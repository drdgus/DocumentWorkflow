namespace DocumentWorkflow.Core.DAL.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public float Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        public int DocumentCategoryId { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }
        public virtual List<History> History { get; set; }
    }
}
