using CommunityToolkit.Mvvm.ComponentModel;

using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public partial class ContractListViewModel
        : ListViewModel<Contract>
    {
        [ObservableProperty]
        Visibility _managerVisibility = Visibility.Visible;

        public ContractListViewModel(IDbService<Contract> service)
        : base(service)
        {
        }

        protected override void EditItem()
        {
            ((ContractService)_service).OpenContract(SelectedItem.Id);
        }

        protected override ItemViewModel<Contract> CreateItemViewModel(IDbService<Contract> service, Contract item)
            => new ContractItemViewModel((ContractService)service, item);

        protected override ItemViewModel<Contract> CreateItemViewModel(IDbService<Contract> service)
            => new ContractItemViewModel((ContractService)service);

        protected override Window CreateItemDialog(ItemViewModel<Contract> itemViewModel)
            => new ContractItemWindow(itemViewModel);
    }
}
