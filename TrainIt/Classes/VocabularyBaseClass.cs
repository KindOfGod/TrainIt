using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrainIt.Annotations;
using TrainIt.ViewModel;

namespace TrainIt.Classes
{
    public class VocabularyBaseClass : BaseViewModel
    {
        #region Fields

        private Visibility _isVisible;
        private bool _isExpanded;

        #endregion
        
        public Guid Id { get; set; }

        public double Grade { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public DateTime LastLearned { get; set; }

        public bool IsNew { get; set; }

        public bool IsExpanded 
        { 
            get => _isExpanded;
            set
            {
                if (_isExpanded == value)
                    return;

                _isExpanded = value;
                OnPropertyChange();
            }
        }

        public Visibility IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value)
                    return;

                _isVisible = value;
                OnPropertyChange();
            }
        }
    }
}
