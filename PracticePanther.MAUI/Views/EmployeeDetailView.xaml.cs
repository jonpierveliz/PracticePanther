using PracticePanther.Library.Models;
using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views;

[QueryProperty(nameof(EmployeeId), "employeeId")]
public partial class EmployeeDetailView : ContentPage
{
    public int EmployeeId { get; set; }
	public EmployeeDetailView()
	{
		InitializeComponent();
    }
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EmployeeViewModel(EmployeeId);
    }

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