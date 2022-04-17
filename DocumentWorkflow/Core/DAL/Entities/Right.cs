namespace DocumentWorkflow.Core.DAL.Entities
{
    public class CategoryRights
    {
        public CategoryRights(int roleId, int? documentCategoryId, bool canWrite)
        {
            RoleId = roleId;
            DocumentCategoryId = documentCategoryId;
            CanWrite = canWrite;
        }
        public bool CanWrite { get; private set; }
        public int RoleId { get; set; }
        public int? DocumentCategoryId { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
