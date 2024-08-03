using System.Collections.ObjectModel;
using System.Text.Json;
using Budgeter.ViewModels;

namespace Budgeter
{
	public partial class MainPage : ContentPage
	{ 
		public MainPage()
		{
			InitializeComponent();
		}

		// dummy code to test collection view splits data
		public static List<Split> SetSplits()
		{
            return
            [
                new Split { Name = "Savings", Value = 400000, Percent = 40 },
				new Split { Name = "Expenses", Value = 300000, Percent = 30 },
				new Split { Name = "Food", Value = 200000, Percent = 20 },
				new Split { Name = "Treats", Value = 100000, Percent = 10 }
			];
		}

		public static ObservableCollection<Split> GetDefaultSplits(string defaultSplit)
		{
			// retrieve json from file system as string
			string myJson = LoadMauiAsset(defaultSplit).Result;

			return JsonSerializer.Deserialize<ObservableCollection<Split>>(myJson);
		}

		static async Task<string> LoadMauiAsset(string asset)
		{
			try
			{
				using var stream = FileSystem.OpenAppPackageFileAsync("default_split.json").Result;
				using var reader = new StreamReader(stream);

				return reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				return "";
			}
		}

		public static string CalculateTotal(ObservableCollection<Split> splits)
		{
			double total = 0;
			for (int i=0; i<splits.Count; i++) {
				total += splits[i].Value;
			}

			string totalFormatted = total.ToString("C", new System.Globalization.CultureInfo("en-US"));

			return totalFormatted; 
		}

		// CSV version
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
