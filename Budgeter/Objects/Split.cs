using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Budgeter
{
	public class Split : ObservableObject
	{
		public string Name { get; set; }
		public decimal Value { get; set; }
		public double Percent { get; set; }
		public int Id { get; set; }
	}
}
