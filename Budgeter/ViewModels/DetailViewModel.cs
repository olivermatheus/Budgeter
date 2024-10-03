using Budgeter.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics.Text;

namespace Budgeter.ViewModels;

[QueryProperty("ThisSplit", "Split")]
public partial class DetailViewModel : ObservableObject
{
    public DetailViewModel() {
        
    }

    [ObservableProperty]
    Split thisSplit;
    [ObservableProperty]
    string name, tempName, tempValue, tempPercent;
    [ObservableProperty]
    decimal value;
    [ObservableProperty]
    double percent;

    [RelayCommand]
    async Task Save() {
        
        if (TempName != null) 
        {
            ThisSplit.Name = TempName;
        }

        if (TempValue != null) {
            ThisSplit.Value = decimal.Parse(TempValue);
        }

        if (TempPercent != null) {
            ThisSplit.Percent = double.Parse(TempPercent);
        }
        WeakReferenceMessenger.Default.Send(new ChangeSplitMessage1(ThisSplit));
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
        WeakReferenceMessenger.Default.Send(new ChangeSplitMessage1(ThisSplit));
        await Shell.Current.GoToAsync("..");
    }

    // delete split
    [RelayCommand]
    async Task Delete() {
        WeakReferenceMessenger.Default.Send(new DeleteSplitMessage(ThisSplit));
        await GoBack();
    }
}
