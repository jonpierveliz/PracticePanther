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
            Shell.Current.GoToAsync("//MainPage");
        }

          private void ViewBillsClicked(object sender, EventArgs e)
        {
            var selectedClient = ((Button)sender).BindingContext as ClientViewModel;

            if (selectedClient != null)
            {
                // Open the BillsView for the selected client
                var window = new Window()
                {
                    Width = 250,
                    Height = 350,
                    X = 0,
                    Y = 0
                };

                var billsView = new BillsView(selectedClient.Model.Id, window);
                window.Page = billsView;

                Application.Current.OpenWindow(window);
            }
        }
    }
}
