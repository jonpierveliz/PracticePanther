using PracticePanther.MAUI.ViewModels;
namespace PracticePanther.MAUI.Views;

public partial class BillsDetailView : ContentPage
{
	public BillsDetailView(int projectId, Window parentWindow)
	{
        InitializeComponent();

        BindingContext = new BillsViewModel(projectId, parentWindow);
	}
}