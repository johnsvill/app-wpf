using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Model
{
    public class Person
    {
        public int PersonID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual OfficeAssignment OfficeAssigment { get; set; }//Realción uno a uno
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }//Relación uno a muchos
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }//Relación uno a muchos
    }
}
