using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace shanuWebAPICRUDSP.Controllers
{
    public class studentsController : ApiController
    {
        studentDBEntities objapi = new studentDBEntities();

       // USP_Student_Select_Result objDBAPI = new USP_Student_Select_Result();


        // to Search Student Details and display the result
        [HttpGet]
        public IEnumerable<USP_Student_Select_Result> Get(string StudentName, string StudentEmail)
        {
            if (StudentName == null)
                StudentName = "";
            if (StudentEmail == null)
                StudentEmail = "";
            return objapi.USP_Student_Select(StudentName, StudentEmail).AsEnumerable();


        }


        // To Insert new Student Details
        [HttpGet]
        public IEnumerable<string> insertStudent(string StudentName, string StudentEmail, string Phone, string Address)
        {           
              return  objapi.USP_Student_Insert(StudentName, StudentEmail, Phone, Address).AsEnumerable();           
        }

        //to Update Student Details
        [HttpGet]
        public IEnumerable<string> updateStudent(int stdID,string StudentName, string StudentEmail, string Phone, string Address)
        {
            return objapi.USP_Student_Update(stdID,StudentName, StudentEmail, Phone, Address).AsEnumerable();
        }


        //to Update Student Details
        [HttpGet]
        public string deleteStudent(int stdID)
        {
            objapi.USP_Student_Delete(stdID);
            objapi.SaveChanges();
            return "deleted";
        }
    }
}
