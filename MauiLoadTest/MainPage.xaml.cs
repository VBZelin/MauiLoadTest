namespace MauiLoadTest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private int _busy = 0;
    public int Busy
    {
        get => _busy;
        set
        {
            _busy = value;
            OnPropertyChanged(nameof(Busy));
        }
    }

    private async void OnNavigate(object sender, EventArgs e)
    {
        Busy++;
        Button btn = (Button)sender;
        btn.IsEnabled = false;
        await Task.Delay(50);
        await Shell.Current.GoToAsync(btn.CommandParameter.ToString());
        btn.IsEnabled = true;
        Busy--;
    }
}
