using CodeLingo.ViewModels;

namespace CodeLingo.Views
{
    public partial class SubscriptionPage : ContentPage
    {
        public SubscriptionPage(SubscriptionViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
