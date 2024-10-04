using Budgeter.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics.Text;

namespace Budgeter.ViewModels;

public partial class NewSplitViewModel : ObservableObject
{
    private Split _split;

    public Split Split
    {
        get => _split;
        set => SetProperty(ref _split, value);
    }

    public NewSplitViewModel(Split thisSplit) {
        Split = thisSplit;
    }

    [ObservableProperty]
    private string _tempName, _tempValue, _tempPercent;

    [RelayCommand]
    private async Task Save() {
        
        if (TempName != null) 
        {
            Split.Name = TempName;
        }

        if (TempValue != null) {
            Split.Value = decimal.Parse(TempValue);
        }

        if (TempPercent != null) {
            Split.Percent = double.Parse(TempPercent);
        }
        WeakReferenceMessenger.Default.Send(new ChangeSplitMessage1(Split));
        //WeakReferenceMessenger.Default.Send(new DeleteSplitMessage(ThisSplit));
        await Shell.Current.GoToAsync("..");
        // , 
		// 	new Dictionary<string, object> {
		// 		{"Split", ThisSplit},
		// 	});
    }

    // return to main screen
    [RelayCommand]
    async Task GoBack() {
        // WeakReferenceMessenger.Default.Send(new ChangeSplitMessage1(ThisSplit));
        await Shell.Current.GoToAsync("..");
    }

    // delete split
    [RelayCommand]
    async Task Delete() {
        WeakReferenceMessenger.Default.Send(new DeleteSplitMessage(Split));
        await GoBack();
    }
}
