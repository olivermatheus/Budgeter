namespace Budgeter
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}

		protected override Window CreateWindow(IActivationState activationState)
		{
			var window = base.CreateWindow(activationState);

			const int newWidth = 925;
			const int newHeight = 660;

			window.Width = newWidth;
			window.Height = newHeight;

			// window.MinimumHeight = window.MaximumHeight = window.Height = newHeight;
			// window.MinimumWidth = window.MaximumWidth = window.Width = newWidth;

			return window;
		}
	}
}
