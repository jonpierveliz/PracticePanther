using PracticePanther.Library.Models;
using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views
{
    public partial class EmployeeView : ContentPage
    {
        public EmployeeView()
        {
            InitializeComponent();
            BindingContext = new EmployeeViewViewModel();
        }

        // Event handler for the home toolbar item 
        private void HomeClicked(object sender, EventArgs e)
        {
            // Navigate to the MainPage
            Shell.Current.GoToAsync("//MainPage");
        }

        // Event handler for adding a employee
        private void AddClicked(object sender, EventArgs e)
        {
            // Navigate to the employee page 
            Shell.Current.GoToAsync("//EmployeeDetail");
    
        }

        // Event handler for the delete button
        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as EmployeeViewViewModel).RefreshEmployeeList();
        }

        // Event handler for the page being navigated to
        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
 
            (BindingContext as EmployeeViewViewModel).RefreshEmployeeList();
        }

        // Event handler for the go back button 
        private void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}
