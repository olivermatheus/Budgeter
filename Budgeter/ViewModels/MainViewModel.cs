using Budgeter.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace Budgeter.ViewModels;

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

		//
		// User Actions 
		// 

		// For dragging and dropping splits
		[RelayCommand]
		private void DragStarting(Split split)
		{
			// Store the item being dragged
			_draggedSplit = split; 
		}

		// For dragging and dropping splits
		[RelayCommand]
		private void Drop(Split targetItem)
		{
			if (_draggedSplit == null || targetItem == null || _draggedSplit == targetItem)
				return;

			// Get the original and new indices
			int originalIndex = SplitsCollection.IndexOf(_draggedSplit);
			int targetIndex = SplitsCollection.IndexOf(targetItem);

			// reorder accordingly
			if (originalIndex != targetIndex)
			{
				SplitsCollection.RemoveAt(originalIndex);
				SplitsCollection.Insert(targetIndex, _draggedSplit);
			}

			// There's still a bug where old names show in certain positions, so this brute forces past this
			RefreshSplitsCollection();

			// Reset the dragged item
			_draggedSplit = null; 
		}

		[RelayCommand]
		async Task OpenSplit(Split thisSplit) { 
			var detailViewModel = new DetailViewModel(thisSplit);
			var detailPage = new DetailPage
			{
				BindingContext = detailViewModel
			};

        	await Shell.Current.Navigation.PushAsync(detailPage);
		}

		[RelayCommand]
		async Task NewSplit() {
			int newId = FindNewID();
			
			Split newSplit = new Split { Name = "--", Value = 0, Percent = 0, Id=newId };
			SplitsCollection.Add(newSplit);
			var detailViewModel = new DetailViewModel(newSplit);
			var detailPage = new DetailPage
			{
				BindingContext = detailViewModel
			};

        	await Shell.Current.Navigation.PushAsync(detailPage);
		}

		// This runs when the user delete the split from the detail page
		void RemoveSplit(Split s) {
			if(SplitsCollection.Contains(s)) {
				SplitsCollection.Remove(s);
			}
		}

		// This function runs when the user makes changes and presses save on the detail page
		void ChangeSplit(Split s) {
			RefreshSplitsCollection();
		}

		//
		// Program tools
		// 

		// This searches through the splits and returns an unused ID number
		int FindNewID() {
			bool idFound = false;
			int[] ids = new int[SplitsCollection.Count];

			// Gather all ids into array
			int counter = 0;
			while(counter < SplitsCollection.Count) {
				ids[counter] = SplitsCollection[counter].Id;
				counter++;
			}
			// Cycle through ids until an unused one is found
			int newId = 1;
			while(idFound==false) {
				if(ids.Contains(newId)) {
					newId++;
				}
				else {
					idFound = true;
				}
			}

			return newId;
		}

		// This function forces the UI to refresh the collection and update the displayed properties
		void RefreshSplitsCollection() {
			Collection<Split> temp = new Collection<Split>();

			for(int i=0; i<SplitsCollection.Count; i++) {
				temp.Add(SplitsCollection[i]);
			}

			SplitsCollection.Clear();
			for(int i=0; i<temp.Count; i++) {
				SplitsCollection.Add(temp[i]);
			}
		}

	}
