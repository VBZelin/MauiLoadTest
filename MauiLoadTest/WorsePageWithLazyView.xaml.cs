namespace MauiLoadTest;

public partial class WorsePageWithLazyView : ContentPage
{
    public WorsePageWithLazyView()
    {
        InitializeComponent();
        _ = LoadLazyViewAsync();
    }

    private async Task LoadLazyViewAsync()
    {
        await Task.Delay(200); // This is needed to simulate a delay in loading the view.
        await LazyUserAction.LoadViewAsync();
    }
}
