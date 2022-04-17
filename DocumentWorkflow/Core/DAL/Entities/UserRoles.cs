namespace DocumentWorkflow.Core.DAL.Entities
{
    public class UserRoles
    {
        public UserRoles(int? userId, int roleId)
        {
            UserId = roleId;
            RoleId = roleId;
        }
        public int? UserId { get; private set; }
        public int RoleId { get; private set; }

        public virtual Role Role { get; set; }


    }
}
