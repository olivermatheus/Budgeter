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
		string totalValue;

        [ObservableProperty]
        ObservableCollection<Split> splitsCollection = [];

		[RelayCommand]
		void SplitTapped() {
			Console.WriteLine("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
		}

		[RelayCommand]
		void AddSplit() {
			splitsCollection.Add(new Split { Name = "TEST", Value = 2, Percent = 0.2 });
		}


	}
}
