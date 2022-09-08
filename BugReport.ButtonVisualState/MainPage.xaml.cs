namespace BugReport.ButtonVisualState;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void ResetButtonVisualState(object sender, EventArgs e)
	{
		// Must call `Unfocus` to reset Button's VisualState to `Normal`.
		(sender as Button).Unfocus();
	}
}

