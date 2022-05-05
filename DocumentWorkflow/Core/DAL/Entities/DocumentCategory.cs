namespace DocumentWorkflow.Core.DAL.Entities
{
    public class DocumentCategory
    {


        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string? CustomTemplateFileName { get; set; }
        public int DocumentTypeId { get; set; }
        public int LogBookId { get; set; }
        public RequiredModule RequiredModule { get; set; }

        public virtual DocumentCategory? ParentCategory { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual LogBook LogBook { get; set; }
    }
}
