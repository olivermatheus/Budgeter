using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace Budgeter.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel() {
			// splitsCollection = MainPage.SetSplits();
			splitsCollection = MainPage.GetDefaultSplits("default_split.json");
			total = MainPage.CalculateTotal(splitsCollection);
        }

        [ObservableProperty]
        string name = "";
        [ObservableProperty]
        double value, percent;
		[ObservableProperty]
		string total;

        [ObservableProperty]
        List<Split> splitsCollection = [];

		// public List<Split> ReadCsv()
		// {
		// 	using var stream = FileSystem.OpenAppPackageFileAsync("default_split.csv").Result;
		// 	using var reader = new StreamReader(stream);
		// 	using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

		// 	var records = csv.GetRecords<Split>().ToList();
		// 	return records;
		// }

		// static async Task<string> LoadMauiAssetCSV(string asset)
		// {
		// 	try
		// 	{
		// 		using var stream = FileSystem.OpenAppPackageFileAsync("default_split.json").Result;
		// 		using var reader = new StreamReader(stream);

		// 		return reader.ReadToEnd();
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		return "";
		// 	}
		// }
	}
}
