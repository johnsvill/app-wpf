using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Model
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public Decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int Administrator { get; set; }
        public  virtual ICollection<Course> Courses { get; set; }//Relación uno a muchos (se aplica la interfaz "ICollection" para extender la colección de datos, pero es válido usar "List")
    }
}
