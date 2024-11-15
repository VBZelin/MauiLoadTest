namespace MauiLoadTest;

public partial class WorsePageWithLazyView : ContentPage
{
    public WorsePageWithLazyView()
    {
        InitializeComponent();
    }

    private async void LoadLazyView_Clicked(object sender, EventArgs e)
    {
        await LazyUserAction.LoadViewAsync();
    }
}
