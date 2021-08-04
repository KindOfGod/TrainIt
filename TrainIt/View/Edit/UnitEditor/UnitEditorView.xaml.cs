using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel.EditModels.UnitEditorModels;

namespace TrainIt.View.Edit.UnitEditor
{
    /// <summary>
    /// Interaction logic for UnitEditorView.xaml
    /// </summary>
    public partial class UnitEditorView : UserControl
    {
        public UnitEditorView()
        {
            InitializeComponent();
            Loaded += ChildLoaded;
        }

        private void ChildLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if(window != null)
                window.Closing += ParentClosing;
        }

        private void ParentClosing(object sender, CancelEventArgs e)
        {
            (DataContext as UnitEditorViewModel)?.SaveChanges();
        }
    }
}
