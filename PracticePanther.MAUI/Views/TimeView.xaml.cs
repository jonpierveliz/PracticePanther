using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI.Views;

public partial class TimeView : ContentPage
{
    public TimeView()
    {
        InitializeComponent();
        BindingContext = new TimeViewViewModel();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        // Navigate to the employee page 
        Shell.Current.GoToAsync("//TimeDetail");

    }

    // Event handler for the delete button
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewViewModel).RefreshTimeEntriesList();
    }

    // Event handler for the page being navigated to
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {

        (BindingContext as TimeViewViewModel).RefreshTimeEntriesList();
    }

    // Event handler for the go back button 
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

}
