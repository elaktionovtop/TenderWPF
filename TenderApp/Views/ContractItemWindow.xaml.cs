using System.Windows;

using TenderApp.Models;
using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public partial class ContractItemWindow : Window
    {
        public ContractItemWindow(ItemViewModel<Contract> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void FirstField_Loaded(object sender,
            RoutedEventArgs e)
        {
            ItemWindowHelper.FocusFirstField(sender);
        }
    }
}
