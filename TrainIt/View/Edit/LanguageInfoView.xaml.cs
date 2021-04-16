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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using TrainIt.Model;
using TrainIt.ViewModel.EditModels;

namespace TrainIt.View.Edit
{
    /// <summary>
    /// Interaction logic for LanguageInfoView.xaml
    /// </summary>
    public partial class LanguageInfoView : UserControl
    {
        public readonly LanguageInfoViewModel _languageInfoViewModel;

        public LanguageInfoView(TrainItService trainItService, IDialogCoordinator dialogCoordinator)
        {
            InitializeComponent();

            _languageInfoViewModel = new LanguageInfoViewModel(trainItService, dialogCoordinator);
            DataContext = _languageInfoViewModel;
        }
    }
}