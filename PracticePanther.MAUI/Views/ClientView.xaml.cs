using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views
{
    public partial class ClientView : ContentPage
    {
        public ClientView()
        {
            InitializeComponent();

            // Set the binding context to an instance of ClientViewViewModel
            BindingContext = new ClientViewViewModel();
        }

        // Event handler for the delete button
        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewViewModel).RefreshClientList();
        }

        // Event handler for the add button
        private void AddClicked(object sender, EventArgs e)
        {
            // Navigate to the ClientDetail page
            Shell.Current.GoToAsync("//ClientDetail");
        }

        // Event handler for the page being navigated to
        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            // Call the RefreshClientList method of the ClientViewViewModel to refresh the client list
            (BindingContext as ClientViewViewModel).RefreshClientList();
        }

        // Event handler for the go back button 
        private void BackClicked(object sender, EventArgs e)
        {
            // Go to main page
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}
