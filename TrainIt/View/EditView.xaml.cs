using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrainIt.Classes;
using TrainIt.ViewModel;

namespace TrainIt.View
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        public EditView()
        {
            InitializeComponent();
        }

        //Rework: Possible MVVM conform solution for selectedItem and selectedContextMenuItem
        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is EditViewModel viewModel)
            {
                viewModel.SelectedItem = e.NewValue;
            }

            if (e.NewValue == null)
                return;

            if (e.NewValue.GetType() == typeof(Language))
            {
                EditTreeView.ContextMenu = EditTreeView.Resources["LanguageContextMenu"] as ContextMenu;
            }
            else if (e.NewValue.GetType() == typeof(Section))
            {
                EditTreeView.ContextMenu = EditTreeView.Resources["SectionContextMenu"] as ContextMenu;
            }
            else
            {
                EditTreeView.ContextMenu = EditTreeView.Resources["UnitContextMenu"] as ContextMenu;
            }
        }

        //Rework: Possible MVVM conform solution for Right Click Event
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseEventArgs e)
        {
            if (DataContext is EditViewModel viewModel)
            {
                var treeViewItem = EditViewModel.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);

                if (treeViewItem == null) 
                    return;

                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }
    }
}
