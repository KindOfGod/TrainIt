using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel.EditModels;

namespace TrainIt.View.Edit
{
    /// <summary>
    /// Interaction logic for SectionInfoView.xaml
    /// </summary>
    public partial class SectionInfoView : UserControl
    {
        private readonly SectionInfoViewModel _sectionInfoViewModel;

        public SectionInfoView(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            InitializeComponent();

            _sectionInfoViewModel = new SectionInfoViewModel(trainItService, dialogCoordinator);
            DataContext = _sectionInfoViewModel;
        }
    }
}
