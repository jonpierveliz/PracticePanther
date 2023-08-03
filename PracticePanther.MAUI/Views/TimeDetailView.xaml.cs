using PracticePanther.Library.Models;
using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views;

[QueryProperty(nameof(TimeEntryId), "timeEntryId")]
public partial class TimeDetailView : ContentPage
{
    // Property for storing time entry id 
    public int TimeEntryId { get; set; }
    public TimeDetailView()
    {
        InitializeComponent();
    }
    
    // Event handler when page is arrived to 
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new TimeViewModel(TimeEntryId);
    }

    // Event handler for ok button
    private void OkClicked(object sender, EventArgs e)
    {
        // Call AddOrUpdate method of the ClientViewModel to save changes
        (BindingContext as TimeViewModel).AddOrUpdate();

        // Navigate back to the client list view
        Shell.Current.GoToAsync("//Time");


    }

    // Event handler for the cancel button
    private void CancelClicked(object sender, EventArgs e)
    {
        // Navigate back to the client list view without saving changes
        Shell.Current.GoToAsync("//Time");
    }
}