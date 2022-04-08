using System.Security.Cryptography;

namespace DocumentWorkflow.Core.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Byte[] Password { get; set; }
        public List<UserRoles> Roles { get; set; }
    }
}
