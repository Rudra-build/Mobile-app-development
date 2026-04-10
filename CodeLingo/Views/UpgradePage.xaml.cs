using CodeLingo.ViewModels;

namespace CodeLingo.Views
{
    public partial class UpgradePage : ContentPage
    {
        public UpgradePage(UpgradeViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
