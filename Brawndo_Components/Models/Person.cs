namespace Brawndo_Components.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Discriminator { get; set; } = string.Empty;
    }

    public class Student : Person
    {
    }

    public class Instructor : Person
    {
    }
}
