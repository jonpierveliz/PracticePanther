using PracticePanther.Library.Models;
using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views;

// QueryProperty attribute for passing employee id as a query parameter
[QueryProperty(nameof(EmployeeId), "employeeId")]
public partial class EmployeeDetailView : ContentPage
{
    // Property for storing the employee id 
    public int EmployeeId { get; set; }
	public EmployeeDetailView()
	{
		InitializeComponent();
    }

    // Event handler when page is arrived to 
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EmployeeViewModel(EmployeeId);
    }

    // Event handler when ok button is clicked
    private void OkClicked(object sender, EventArgs e)
    {
        // Call AddOrUpdate method of the ClientViewModel to save changes
        (BindingContext as EmployeeViewModel).AddOrUpdate();

        // Navigate back to the client list view
        Shell.Current.GoToAsync("//Employee");


    }

    // Event handler for the cancel button
    private void CancelClicked(object sender, EventArgs e)
    {
        // Navigate back to the client list view without saving changes
        Shell.Current.GoToAsync("//Employee");
    }
}