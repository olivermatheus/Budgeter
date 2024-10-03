using Budgeter.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace Budgeter.ViewModels;

//[QueryProperty("SplitEdit","Split")]
    public partial class MainViewModel : ObservableObject
    {
		private Split _draggedSplit;

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
		private void DragStarting(Split split)
		{
			_draggedSplit = split; // Store the item being dragged
		}

		[RelayCommand]
		private void Drop(Split targetItem)
		{
			if (_draggedSplit == null || targetItem == null || _draggedSplit == targetItem)
				return;

			// get the original and new indices
			int originalIndex = SplitsCollection.IndexOf(_draggedSplit);
			int targetIndex = SplitsCollection.IndexOf(targetItem);

			// reorder accordingly
			if (originalIndex != targetIndex)
			{
				SplitsCollection.RemoveAt(originalIndex);
				SplitsCollection.Insert(targetIndex, _draggedSplit);
			}

			_draggedSplit = null; // Reset the dragged item
		}

		[RelayCommand]
		async Task OpenSplit(Split thisSplit) { 
			int tempId = thisSplit.Id;
			var itemToRemove = SplitsCollection.FirstOrDefault(s => s.Id == tempId);

			// if found, remove it from the collection
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
			int splitCount = SplitsCollection.Count;
			bool idFound = false;
			int[] ids = new int[splitCount];

			// gather all ids into array
			int counter = 0;
			while(counter < splitCount) {
				ids[counter] = SplitsCollection[counter].Id;
				counter++;
			}
			// cycle through ids until an unused one is found
			int newId = 1;
			while(idFound==false) {
				if(ids.Contains(newId)) {
					newId++;
				}
				else {
					idFound = true;
				}
			}
			
			Split newSplit = new Split { Name = "--", Value = 0, Percent = 0, Id=newId };
			await Shell.Current.GoToAsync(nameof(NewSplitPage));

			// await Shell.Current.GoToAsync(nameof(NewSplitPage), 
			// new Dictionary<string, object> {
			// 	{"Split", newSplit},
			// });
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
