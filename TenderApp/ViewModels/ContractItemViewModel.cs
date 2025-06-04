using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TenderApp.Models;

using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class ContractItemViewModel 
        : ItemViewModel<Contract>
    {

        public ContractItemViewModel(ContractService service)
            : base(service)
        {
        }

        public ContractItemViewModel(ContractService service,
            Contract item)
            : base(service, item)
        {
        }
    }
}

