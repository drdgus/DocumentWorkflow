namespace DocumentWorkflow.Core.DAL.Entities
{
    public class UserRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
