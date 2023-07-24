using PracticePanther.MAUI.ViewModels;

namespace PracticePanther.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        private void HomeClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
        private void ClientClicked (object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Client");
        }

        private void EmployeeClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Employee");
        }

        private void TimeClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Time");
        }

        private void BillsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Bills");
        }
    }
}
           