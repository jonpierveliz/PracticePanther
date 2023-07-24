using PracticePanther.MAUI.Views;
using PracticePanther.MAUI.ViewModels;
using PracticePanther.Library.Services;
using PracticePanther.Library.Models;

namespace PracticePanther.MAUI.Views;

[QueryProperty(nameof(ProjectId), "projectId")]
[QueryProperty(nameof(ClientId), "clientId")]
public partial class ProjectDetailView : ContentPage
{
    public int ProjectId { get; set; }
    public int ClientId { get; set; }

    public ProjectDetailView()
    {
        InitializeComponent();
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ClientId, ProjectId);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//ClientDetail?clientId={ClientId}");
    }

    private void OkClicked(object sender, EventArgs e)
    {

        (BindingContext as ProjectViewModel).AddOrUpdate();
        Shell.Current.GoToAsync($"//ClientDetail?clientId={ClientId}");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}
