namespace Brawndo_Components.Models
{
    public class OfficeAssignment
    {
        public int InstructorID { get; set; }
        public string Location { get; set; } = string.Empty;
        public byte[] Timestamp { get; set; } = Array.Empty<byte>();
    }
}
