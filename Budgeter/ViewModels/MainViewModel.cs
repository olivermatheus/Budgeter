using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Budgeter.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel() {
			// splitsCollection = MainPage.SetSplits();
			splitsCollection = MainPage.GetDefaultSplits("default_split.json");
			totalValue = MainPage.CalculateTotal(splitsCollection);
        }

        [ObservableProperty]
        string name = "";
        [ObservableProperty]
        double value, percent;
		[ObservableProperty]
		decimal totalValue;

        [ObservableProperty]
        ObservableCollection<Split> splitsCollection = [];

		[RelayCommand]
		async void OpenSplit(Split split) {
			// splitsCollection.Add(new Split { Name = "OPEN SUCCESS", Value = 0, Percent = 0 });

			await Shell.Current.GoToAsync($"{nameof(DetailPage)}", 
			new Dictionary<string, object> {
				{nameof(DetailPage), split},
			});
		}

		[RelayCommand]
		void AddSplit() {
			SplitsCollection.Add(new Split { Name = "TEST", Value = 2, Percent = 0.2, ID=3 });
		}

		[RelayCommand]
		void RemoveSplit(Split s) {
			if(SplitsCollection.Contains(s)) {
				SplitsCollection.Remove(s);
			}
		}

	}
}
