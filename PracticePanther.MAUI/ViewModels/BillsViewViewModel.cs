using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PracticePanther.MAUI.ViewModels
{
    internal class BillsViewViewModel
    {
        public Bill SelectedBill { get; set; }

        public int ProjectId;

        public ObservableCollection<BillsViewModel> Bills { get; }

        public BillsViewViewModel()
        {
            Bills = new ObservableCollection<BillsViewModel>(
                BillService.Current.Bills.Select(bill => new BillsViewModel(bill)));
        }

        public BillsViewViewModel(int projectId)
        {
            ProjectId = projectId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public void RefreshBillsList()
        {
            NotifyPropertyChanged(nameof(Bills));
        }

    }
}
