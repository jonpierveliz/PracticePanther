using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views
{
    // QueryProperty attribute for passing client id as a query parameter
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class ClientDetailView : ContentPage
    {
        // Property for storing the client id
        public int ClientId { get; set; }

        public ClientDetailView()
        {
            InitializeComponent();
        }

        // Event handler for the ok button 
        private void OkClicked(object sender, EventArgs e)
        {
            // Call AddOrUpdate method of the ClientViewModel to save changes
            (BindingContext as ClientViewModel).AddOrUpdate();

            // Navigate back to the client list view
            Shell.Current.GoToAsync("//Client");
        }

        // Event handler for the cancel button
        private void CancelClicked(object sender, EventArgs e)
        {
            // Navigate back to the client list view without saving changes
            Shell.Current.GoToAsync("//Client");
        }

        // Event handler for when the page is navigated to
        private void OnArriving(object sender, NavigatedToEventArgs e)
        {
            // Create a new instance of ClientViewModel and set it as the binding context
            BindingContext = new ClientViewModel(ClientId);
        }

        private void DeleteProjectClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewModel).RefreshProjectList();
        }

        // Event handler for the edit button 
        private void EditClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewModel).RefreshProjectList();
        }


        private void OpenBillsWindowClicked(object sender, EventArgs e)
        {
            var selectedProject = ((Button)sender).BindingContext as ProjectViewModel;

            if (selectedProject != null)
            {
                var window = new Window()
                {
                    Width = 380,
                    Height = 350,
                    X = 0,
                    Y = 0
                };

                var billsView = new BillsView(selectedProject.Model.Id, window);
                window.Page = billsView;

                Application.Current.OpenWindow(window);
            }
        }

    }
}
