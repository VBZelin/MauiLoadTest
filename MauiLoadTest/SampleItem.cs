using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiLoadTest;

public partial class SampleItem : ObservableObject
{
    private readonly static Random random = new();

    [ObservableProperty]
    private string _id = String.Empty;

    [ObservableProperty]
    private string _title = String.Empty;

    [ObservableProperty]
    private DateTime _modified = DateTime.Now;

    [ObservableProperty]
    private float _w = 426;

    [ObservableProperty]
    private float _h = 20;
    public void Randomize()
    {
        Id = Guid.NewGuid().ToString();
        Title = "Title " + Id;
        Modified = DateTime.Now;
    }

    public SampleItem()
    {
        Randomize();
    }
}
