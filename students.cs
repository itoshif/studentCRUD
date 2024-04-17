namespace WebApplication1.Models
{
    public class students
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address {  get; set; }
        
    }
    public class Connection
    {
        public string MasterPGConnection {  get; set; }
    }
}
