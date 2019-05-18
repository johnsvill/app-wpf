using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWPF.Model;

namespace AppWPF.ModelView
{
    public class DepartmentViewModel : INotifyPropertyChanged //Interfaz
    {
        private ObservableCollection<Department> _Department;
        public ObservableCollection<Department> Departments
        {
            get { return this._Department; }
            set { this._Department = value; }
        }
        public DepartmentViewModel()
        {
            this.Titulo = "Lista de Departamentos:";//Se inicializa el nombre del título
        }
        public string Titulo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
