using System;
using System.Collections.Generic;
using System.Data.Entity;//Instalación de Entity Framework
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.Model//FLUENT API
{
    //Esta es la capa de acceso a datos
    public class SchoolDataContext : DbContext//Se enlaza el namespace con el Entity Framework
    {
        public DbSet<Department> Departments { get; set; }//Pluraliza por defecto en inglés
        public DbSet<Course> Courses { get; set; }
        public DbSet<OnlineCourse> OnlineCourses { get; set;}
        public DbSet<OnsiteCourse> OnsiteCourses { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<OfficeAssignment> OfficeAssigments { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//Para quitar pluralización
            //MAPEOS
            modelBuilder.Entity<Department>()//Modelo de lado de C# "MAPEO"
                .ToTable("Department")//Fluent API y modelo del lado de SQL
                .HasKey(d => new { d.DepartmentID })//Para enlazar la llave primaria
                .Property(d => d.Name)//Para definir un campo requerido
                .IsRequired()
                .HasMaxLength(128);//Para definir el largo de caracteres de la propiedad 

            modelBuilder.Entity<Department>()
                .ToTable("Department")
                .Property(d => d.StartDate)
                .IsRequired();

            modelBuilder.Entity<Course>()//MAPEO
                .ToTable("Course")
                .HasKey(c => new { c.CourseId })
                .Property(c => c.Title)
                .IsRequired();
            modelBuilder.Entity<Course>()//Para indicar cual es la llave primaria si las 2 tablas tienen el mismo nombre
                .HasRequired(c => c.OnlineCourse)
                .WithRequiredPrincipal(c => c.Course);
            modelBuilder.Entity<Course>()//Para indicar cual es la llave primaria si las 2 tablas tienen el mismo nombre
                .HasRequired(c => c.OnsiteCourse)
                .WithRequiredPrincipal(c => c.Course);

            modelBuilder.Entity<OnlineCourse>()//MAPEO
                .ToTable("OnlineCourse")
                .HasKey(o => new { o.CourseID })
                .Property(o => o.URL)
                .IsRequired();

            modelBuilder.Entity<OnsiteCourse>()//MAPEO
                .ToTable("OnsiteCourse")
                .HasKey(o => new { o.CourseID })
                .Property(o => o.Location)
                .IsRequired();

            modelBuilder.Entity<Person>()//MAPEO
                .ToTable("Person")
                .HasKey(o => new { o.PersonID })
                .Property(o => o.LastName)
                .IsRequired();
            modelBuilder.Entity<Person>()
                .Property(o => o.FirstName)
                .IsRequired();
            modelBuilder.Entity<Person>()//Para indicar cual es la llave primaria si las 2 tablas tienen el mismo nombre
                .HasRequired(p => p.OfficeAssigment)
                .WithRequiredPrincipal(p => p.Person);

            modelBuilder.Entity<OfficeAssignment>()//MAPEO
                .ToTable("OfficeAssignment")
                .HasKey(o => new { o.InstructorID });

            modelBuilder.Entity<StudentGrade>()//MAPEO
                .ToTable("StudentGrade")
                .HasKey(x => new { x.EnrollmentID });

            //Procedimiento para hacer referencia cuando la llave foránea no se llama igual q la llave primaria de la propiedad del objeto
            modelBuilder.Entity<StudentGrade>()//Se hace este procedimiento cuando la llave foránea no se llama igual q la llave primaria en la otra tabla
                .ToTable("StudentGrade")
                .HasRequired<Person>(p => p.Person)//Se hace la referencia al objeto Person
                .WithMany(p => p.StudentGrades)
                .HasForeignKey<int>(s => s.StudentID);

            //Procedimiento para cuando las llaves primarias de 2 tablas no se llamen igual
           /* modelBuilder.Entity<OfficeAssignment>()
                .HasRequired(p => p.Person)
                .WithOptional(p => p.OfficeAssigment)
                .Map(o => o.MapKey("InstructorID"));*/

            modelBuilder.Entity<CourseInstructor>()//MAPEO
                .ToTable("CourseInstructor")
                .HasKey(c => new { c.CourseID, c.PersonID });//Llave primaria compuesta

            modelBuilder.Entity<OfficeAssignment>()
                .ToTable("OfficeAssignment")
                .HasKey(o => o.InstructorID);
        }
    }
}
