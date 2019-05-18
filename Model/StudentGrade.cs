using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Model
{
    public class StudentGrade
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int Grade { get; set; }
        public virtual Person Person { get; set; }//Relación muchos a uno
        public virtual Course Course { get; set; }//Relación muchos a uno
    }
}
