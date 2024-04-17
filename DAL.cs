
using WebApplication1.Models;
using Dapper;
using Npgsql;
using System.Numerics;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Repository

{
    public class DAL
    {
        private readonly string connectionstring;
        public DAL(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("PostgresConnection");
        }
        [HttpGet]
        public IEnumerable<students> getAll(string studentfname)
        {
            using (var connection=new NpgsqlConnection(connectionstring))
            {
                return connection.Query<students>("SELECT * from searchall(@studentfirstname)",new { studentfirstname = studentfname }).ToList();
            }
        }
        //
        public IEnumerable<Getmarkmodel> getMarks(int studentid)
        {
            using (var connection=new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                return connection.Query<Getmarkmodel>("SELECT * from getmarks(@pmarkstudentid)", new { pmarkstudentid = studentid }).ToList();

            }
        }
        public bool getFees(int studentid)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                return connection.QueryFirst<bool>("SELECT * from getfees(@pstudentid)", new { pstudentid = studentid });
            }
        }
        public void  addFees(int id,bool status) 
        {
            using (var connection=new NpgsqlConnection( connectionstring))
            {
                connection.Open();
                connection.Execute("CALL addfees(@pstudentid,@pfeestatus)", new { pstudentid = id, pfeestatus = status});
            }
        }
        public void addStudent(int rollNumber, string fname, string lname, DateTime DOB, string Fname, string Mname, string Address)
        {
            using (var connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                connection.Execute("CALL addstudent (@pid, @pfname, @plname, @pdob, @pfathername, @pmothername, @paddress)", new { pid = rollNumber, pfname = fname, plname = lname, pdob = DOB, pfathername = Fname, pmothername = Mname, paddress = Address });
            }
        }

        public void addMarks(int studentid,string[] subject,int[] marks)
        {
            using (var connection=new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                connection.Execute("CALL addmarks(@psubject,@pmarks,@pmarkstudentid)", new { psubject =subject, pmarkstudentid = studentid, pmarks = marks });
            }
        }
    }
}
