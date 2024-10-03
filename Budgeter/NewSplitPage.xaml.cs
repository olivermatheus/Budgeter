namespace Budgeter;

public partial class NewSplitPage : ContentPage
{
	public NewSplitPage(NewSplitPage vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}