using PracticePanther.MAUI.Views;
using PracticePanther.MAUI.ViewModels;
using PracticePanther.Library.Services;
using PracticePanther.Library.Models;

namespace PracticePanther.MAUI.Views;

// QueryProperty attribute for passing project and client id as a query parameter
[QueryProperty(nameof(ProjectId), "projectId")]
[QueryProperty(nameof(ClientId), "clientId")]
public partial class ProjectDetailView : ContentPage
{
    // Property for storing the project id 
    public int ProjectId { get; set; }

    // Property for storing the client id 
    public int ClientId { get; set; }

    public ProjectDetailView()
    {
        InitializeComponent();
    }

    // Event handler when page is arrived to 
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ClientId, ProjectId);
    }

    // Event handler for cancel button
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//ClientDetail?clientId={ClientId}");
    }

    // Event handler for ok button
    private void OkClicked(object sender, EventArgs e)
    {

        (BindingContext as ProjectViewModel).AddOrUpdate();
        Shell.Current.GoToAsync($"//ClientDetail?clientId={ClientId}");
    }
}
