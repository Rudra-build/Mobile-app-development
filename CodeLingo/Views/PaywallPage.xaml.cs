using CodeLingo.ViewModels;

namespace CodeLingo.Views
{
    public partial class PaywallPage : ContentPage
    {
        public PaywallPage(PaywallViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
