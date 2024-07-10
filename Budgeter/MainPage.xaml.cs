using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text.Json;

namespace Budgeter
{
	public partial class MainPage : ContentPage
	{ 
		public MainPage()
		{
			InitializeComponent();

			// Call function to read in splits and assign to CollectionView
			List<Split> splits = GetDefaultSplits("default_split.json");
			//List<Split> splits = ReadCsv();
			 splitsCollection.ItemsSource = splits;
			totalDouble.Text = CalculateTotal(splits);
		}

		// Dummy data for testing
		public List<Split> GetSplits()
		{
			return new List<Split>
			{
				new Split { Name = "Savings", Value = 400000, Percent = 40 },
				new Split { Name = "Expenses", Value = 300000, Percent = 30 },
				new Split { Name = "Food", Value = 200000, Percent = 20 },
				new Split { Name = "Treats", Value = 100000, Percent = 10 }
			};
		}

		private static List<Split> GetDefaultSplits(string defaultSplit)
		{
			// retrieve json from file system as string
			string myJson = LoadMauiAsset(defaultSplit).Result;

			return JsonSerializer.Deserialize<List<Split>>(myJson);
		}

		public List<Split> ReadCsv()
		{
			using var stream = FileSystem.OpenAppPackageFileAsync("default_split.csv").Result;
			using var reader = new StreamReader(stream);
			using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

			var records = csv.GetRecords<Split>().ToList();
			return records;
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

		static async Task<string> LoadMauiAssetCSV(string asset)
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

		string CalculateTotal(List<Split> splits)
		{
			double total = 0;
			for (int i=0; i<splits.Count; i++) {
				total += splits[i].Value;
			}

			string totalFormatted = total.ToString("C", new System.Globalization.CultureInfo("en-US"));

			return totalFormatted; 
		}
	}
}
