using System.Collections.ObjectModel;

namespace MauiLoadTest;

public partial class WorsePage : ContentPage
{
	public ObservableCollection<SampleItem> SampleItems { get; } = new();

	public WorsePage()
	{
		InitializeComponent();
        LoadItems();
    }

    public void LoadItems()
    {
        for (int i = 0; i < 12345; i++)
        {
            SampleItems.Add(new SampleItem());
            if (i % 100 == 0)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(20));
            }
        }
    }
}
