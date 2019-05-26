using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppWPF.Model;

namespace AppWPF.ModelView
{
    public class DepartmentViewModel : INotifyPropertyChanged, ICommand //Interfaz
    {
        //Enlace a la base de datos
        private SchoolDataContext db = new SchoolDataContext();

        private bool _IsReadOnlyName = true;
        private bool _IsReadOnlyBudget = true;
        private bool _IsReadOnlyAdmin = true;
        private string _Name;
        private string _Budget;
        private string _Admin;

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
                ChangeNotify("Name");
            }
        }
        public string Budget
        {
            get
            {
                return this._Budget;
            }
            set
            {
                this._Budget = value;
                ChangeNotify("Budget");
            }
        }
        public string Admin
        {
            get
            {
                return this._Admin;
            }
            set
            {
                this._Admin = value;
                ChangeNotify("Admin");
            }
        }
        private DepartmentViewModel _Instancia;

        /*public DepartmentViewModel()
        {
            this.Instancia = this;//Se hace referencia a la instancia que se creo
        }*/

        private ObservableCollection<Department> _Departments;

        public DepartmentViewModel Instancia
        {
            get
            {
                return this._Instancia;
            }
            set
            {
                this._Instancia = value;
            }
        }
        public Boolean IsReadOnlyName
        {
            get
            {
                return this._IsReadOnlyName;
            }
            set
            {
                this._IsReadOnlyName = value;
                ChangeNotify("IsReadOnlyName");
            }
        }
        public Boolean IsReadOnlyBudget
        {
            get
            {
                return this._IsReadOnlyBudget;
            }
            set
            {
                this._IsReadOnlyBudget = value;
                ChangeNotify("IsReadOnlyBudget");
            }
        }
        public Boolean IsReadOnlyAdmin
        {
            get
            {
                return this._IsReadOnlyAdmin;
            }
            set
            {
                this._IsReadOnlyAdmin = value;
                ChangeNotify("IsReadOnlyAdmin");
            }

        }

        public ObservableCollection<Department> Departments//Propiedad
        {
            get
            {
                if(this._Departments == null)
                {
                    this._Departments = new ObservableCollection<Department>();
                    foreach(Department elemento in db.Departments.ToList())
                    {
                        this._Departments.Add(elemento);
                    }
                }
                return this._Departments;
            }
            set { this._Departments = value; }
        }
        public DepartmentViewModel()
        {
            this.Titulo = "Lista de Departamentos:";//Se inicializa el nombre del título
            this.Instancia = this;//Se hace referencia a la instancia que se creo
        }
        public string Titulo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void ChangeNotify(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.Equals("Add"))
            {
                /*MessageBox.Show("Agregar Departamento");*/
                this.IsReadOnlyName = false;
                this.IsReadOnlyBudget = false;
                this.IsReadOnlyAdmin = false;
            }
            if(parameter.Equals("Save"))
            {
                Department nuevo = new Department();
                nuevo.Name = this.Name;
                nuevo.Budget = Convert.ToDecimal(this.Budget);
                nuevo.Administrator = Convert.ToInt16(this.Admin);
                nuevo.StartDate = DateTime.Now;
                db.Departments.Add(nuevo);
                db.SaveChanges();
                this.Departments.Add(nuevo);
                MessageBox.Show("Registro Almacenado");
            }
        }
    }
}
