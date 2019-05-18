﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AppWPF.ModelView;
using AppWPF.View;

namespace AppWPF.View
{
    /// <summary>
    /// Lógica de interacción para DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : Window
    {
        public DepartmentView()
        {
            InitializeComponent();
            this.DataContext = new DepartmentViewModel();//Para enlazar el archivo del mismo nombre sin la extensión .cs (Modelo de datos"C#" con la ventana "WPF")
        }
    }
}
