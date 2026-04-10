using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeLingo.ViewModels
{
    public partial class PaywallViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task GoToUpgrade() =>
            await Shell.Current.GoToAsync("UpgradePage");

        [RelayCommand]
        private async Task GoBack() =>
            await Shell.Current.GoToAsync("..");
    }
}
