using System.Collections.ObjectModel;

namespace MauiLoadTest;

/// <summary>
/// This is page demonstrates the common problem of doing too much during page navigation/construction.
/// </summary>
public partial class WorsePage : ContentPage
{
    /// <summary>
    /// This collection is bound to the CollectionView and must be updated in the UI thread.
    /// </summary>
	public ObservableCollection<SampleItem> SampleItems { get; } = new();

    /// <summary>
    /// Demonstrates the common problem of doing too much during page navigation/construction.
    /// </summary>
	public WorsePage()
    {
        InitializeComponent();
        LoadItems();
    }

    /// <summary>
    /// Simulate loading 12345 records from a REST service in 100 record pages blocking the UI thread for 3-6 seconds.
    /// </summary>
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
