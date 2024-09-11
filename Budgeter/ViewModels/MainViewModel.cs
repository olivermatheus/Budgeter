using Budgeter.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace Budgeter.ViewModels;

//[QueryProperty("SplitEdit","Split")]
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel() {
			// splitsCollection = MainPage.SetSplits();
			SplitsCollection = MainPage.GetDefaultSplits("default_split.json");
			totalValue = MainPage.CalculateTotal(SplitsCollection);

			WeakReferenceMessenger.Default.Register<DeleteSplitMessage>(this, (r, m) => 
			{
				MainThread.BeginInvokeOnMainThread(() =>{
					RemoveSplit(m.Value);
				});
			});
			WeakReferenceMessenger.Default.Register<ChangeSplitMessage1>(this, (r, m) => 
			{
				MainThread.BeginInvokeOnMainThread(() =>{
					ChangeSplit(m.Value);
				});
			});
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
		async Task OpenSplit(Split thisSplit) { 
			int tempId = thisSplit.Id;
			var itemToRemove = SplitsCollection.FirstOrDefault(s => s.Id == tempId);

			// If found, remove it from the collection
			if (itemToRemove != null)
			{
				SplitsCollection.Remove(itemToRemove);
			}
			await Shell.Current.GoToAsync(nameof(DetailPage), 
			new Dictionary<string, object> {
				{"Split", thisSplit},
			});
		}

		[RelayCommand]
		async Task NewSplit() {
			// SplitsCollection.Add(new Split { Name = "TEST", Value = 2, Percent = 0.2, Id=4 });
			int counter = 0, newId = 0;
			bool idFound = false;
			int[] ids = new int[SplitsCollection.Count];

			// iterate through existing IDs until an unused integer is found
			while(counter < SplitsCollection.Count) {
				ids[counter] = SplitsCollection[counter].Id;
				counter++;
			}
			
			counter=0;
			while(idFound == false) {
				newId++;
				if(ids[counter] != newId) {
					idFound=true;
				}
				counter++;
			}
			
			
			Split newSplit = new Split { Name = "--", Value = 0, Percent = 0, Id=newId };

			await Shell.Current.GoToAsync(nameof(DetailPage), 
			new Dictionary<string, object> {
				{"Split", newSplit},
			});
		}

		void RemoveSplit(Split s) {
			if(SplitsCollection.Contains(s)) {
				SplitsCollection.Remove(s);
			}
		}

		void ChangeSplit(Split s) {
			
			SplitsCollection.Add(s);
		}

	}
