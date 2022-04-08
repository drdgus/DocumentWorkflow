namespace DocumentWorkflow.Core.DAL.Entities
{
    public class Right
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int DocumentCategoryId { get; set; }
        public bool CanWrite { get; set; }
    }
}
