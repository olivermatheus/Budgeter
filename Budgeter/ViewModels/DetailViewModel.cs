using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Budgeter.ViewModels;

[QueryProperty("Split", "Split")]
public partial class DetailViewModel : ObservableObject
{
    public DetailViewModel() {
        
    }

    [ObservableProperty]
    Split split;
    [ObservableProperty]
    string name;
    [ObservableProperty]
    decimal value = 0;
    [ObservableProperty]
    double percent = 0;
    [ObservableProperty]
    string test = "Test Text";

    [RelayCommand]
    async Task GoBack() {
        await Shell.Current.GoToAsync("..");
    }
}
