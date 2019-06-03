using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppWPF.Model;

namespace AppWPF.ModelView
{
    enum ACCION
    {
        NINGUNO,
        NUEVO,
        ACTUALIZAR,
        GUARDAR
    };
    public class DepartmentViewModel : INotifyPropertyChanged, ICommand //Interfaz
    {
        //Enlace a la base de datos
        private SchoolDataContext db = new SchoolDataContext();

        private ACCION accion = ACCION.NINGUNO;
        private bool _IsReadOnlyName = true;
        private bool _IsReadOnlyBudget = true;
        private bool _IsReadOnlyAdmin = true;
        private bool _IsEnabledAdd = false;
        private bool _IsEnabledDelete = false;
        private bool _IsEnabledUpdate = false;
        private bool _IsEnabledSave = true;
        private bool _IsEnabledCancel = true;
        private string _Name;
        private string _Budget;
        private string _Admin;
        private Department _SelectDepartment;

        public bool IsEnabledAdd
        {
            get
            {
                return this._IsEnabledAdd;
            }
            set
            {
                this._IsEnabledAdd = value;
                ChangeNotify("IsEnabledAdd");
            }
        }
        public bool IsEnabledDelete
        {
            get
            {
                return this._IsEnabledDelete;
            }
            set
            {
                this._IsEnabledDelete = value;
                ChangeNotify("IsEnabledDelete");
            }
        }
        public bool IsEnabledUpdate
        {
            get
            {
                return this._IsEnabledUpdate;
            }
            set
            {
                this._IsEnabledUpdate = value;
                ChangeNotify("IsEnabledUpdate");
            }
        }
        public bool IsEnabledSave
        {
            get
            {
                return this._IsEnabledSave;
            }
            set
            {
                this._IsEnabledSave = value;
                ChangeNotify("IsEnabledSave");
            }
        }
        public bool IsEnabledCancel
        {
            get
            {
                return this._IsEnabledCancel;
            }
            set
            {
                this._IsEnabledCancel = value;
                ChangeNotify("IsEnabledCancel");
            }
        }

        public Department SelectDepartment//Propiedad
        {
            get
            {
                return this._SelectDepartment;
            }
            set
            {
                if(value != null)
                {
                    this._SelectDepartment = value;
                    this.Name = value.Name;//Para mostrar la información cuando se seleccione una fila en el Grid
                    this.Budget = value.Budget.ToString();//Para mostrar la información cuando se seleccione una fila en el Grid
                    this.Admin = value.Administrator.ToString();//Para mostrar la información cuando se seleccione una fila en el Grid
                    ChangeNotify("SelectDepartment"); 
                }                
            }
        }
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

        //Interfaz ICommand
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
                this.accion = ACCION.NUEVO;
                this.IsEnabledAdd = false;
                this.IsEnabledDelete = false;
                this.IsEnabledUpdate = false;
                this.IsEnabledSave = true;
                this.IsEnabledCancel = true;
            }
           if(parameter.Equals("Save"))
            {
                switch(this.accion)
                {
                    case ACCION.NUEVO:
                        Department nuevo = new Department();
                        nuevo.Name = this.Name;
                        nuevo.Budget = Convert.ToDecimal(this.Budget);
                        nuevo.Administrator = Convert.ToInt16(this.Admin);
                        nuevo.StartDate = DateTime.Now;
                        db.Departments.Add(nuevo);
                        db.SaveChanges();
                        this.Departments.Add(nuevo);
                        MessageBox.Show("Registro Almacenado");
                        break;
                    case ACCION.ACTUALIZAR:
                        try
                        {
                            int posicion = this.Departments.IndexOf(this.SelectDepartment);//Devuelve la posición del objeto, para que no solo se refleje el cambio en la BD sino también en la vista
                            var updateDepartment = this.db.Departments.Find(this.SelectDepartment.DepartmentID);//OBJETO PERSISTENTE "Significa que la información de la vista tiene q modificarse también en la BD"
                            updateDepartment.Name = this.Name;
                            updateDepartment.Budget = Convert.ToDecimal(this.Budget);
                            updateDepartment.Administrator = Convert.ToInt16(this.Admin);
                            this.db.Entry(updateDepartment).State = EntityState.Modified;
                            this.db.SaveChanges();
                            this.Departments.RemoveAt(posicion);
                            this.Departments.Insert(posicion, updateDepartment);
                            MessageBox.Show("¡Registro actualizado!");
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;
                }                
            }
            else if(parameter.Equals("Update"))
            {
                this.accion = ACCION.ACTUALIZAR;
                this.IsReadOnlyName = false;
                this.IsReadOnlyBudget = false;
                this.IsReadOnlyAdmin = false;
            }
            else if(parameter.Equals("Delete"))
            {
                if(this.SelectDepartment != null)
                {
                    var respuesta = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButton.YesNo);
                    if(respuesta == MessageBoxResult.Yes)
                    {
                        try
                        {
                            db.Departments.Remove(this.SelectDepartment);
                            db.SaveChanges();
                            this.Departments.Remove(this.SelectDepartment);
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        MessageBox.Show("¡Registro eliminado correctamente!");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               /* Name = this.SelectDepartment.Name;
                Budget = this.SelectDepartment.Budget.ToString("0.00");
                Admin = this.SelectDepartment.Administrator.ToString();*/
            }
        }
    }
}
