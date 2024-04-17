namespace WebApplication1.Models
{
    public class Marksmodel
    {
        public int MarksId { get; set; }
        public int StudentId { get; set; }
        public string[] Subject { get; set; }
        public int[] Marks { get; set; }
    }
}