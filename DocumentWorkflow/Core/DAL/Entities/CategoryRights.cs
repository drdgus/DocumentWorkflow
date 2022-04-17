namespace DocumentWorkflow.Core.DAL.Entities
{
    public class CategoryRights
    {
        public int Id { get; set; }
        public bool CanWrite { get; set; }
        public int RoleId { get; set; }
        public int? DocumentCategoryId { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
