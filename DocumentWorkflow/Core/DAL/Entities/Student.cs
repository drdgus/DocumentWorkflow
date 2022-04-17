namespace DocumentWorkflow.Core.DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Class { get; set; }
        public Genders Gender { get; set; }

        public enum Genders
        {
            Male,
            Female
        }
    }
}
