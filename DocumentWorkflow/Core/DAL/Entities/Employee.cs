namespace DocumentWorkflow.Core.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Position { get; set; }
    }
}
