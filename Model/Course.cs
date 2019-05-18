using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Model
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }//Relación unidimensional, relación muchos a uno
        public virtual OnlineCourse OnlineCourse { get; set; }//Relación uno a uno
        public virtual OnsiteCourse OnsiteCourse { get; set; }//Relación uno a uno
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }//Relación uno a muchos
        public virtual ICollection<CourseInstructor> GetCourseInstructors { get; set; }//Relación uno a muchos
    }
}
