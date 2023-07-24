using PracticePanther.MAUI.ViewModels;
using Microsoft.Maui.Controls;

namespace PracticePanther.MAUI.Views
{
    public partial class BillsView : ContentPage
    {
        public BillsView(int projectId, Window parentWindow)
        {
            InitializeComponent();

            BindingContext = new BillsViewModel(projectId, parentWindow);
        }

        
    }
}
