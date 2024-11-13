using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiLoadTest;

/// <summary>
/// This demonstrates some practices to help improve page navigation responsiveness.
/// </summary>
public partial class BestPage : ContentPage
{
    /// <summary>
    /// This is a collection that will be populated in a different thread.
    /// </summary>
    Queue<SampleItem> SampleItemsQueue = new();

    /// <summary>
    /// This is a collection that is linked to a CollectionView and will be intermittent populated in the UI thread.
    /// </summary>
    public ObservableCollection<SampleItem> SampleItems { get; } = new();

    /// <summary>
    /// This is a reference counter that lets us know if we in the process of populating the collection.
    /// </summary>
    private int Loading = 0;

    /// <summary>
    /// This cancellation token will be used to abort any loading.
    /// </summary>
    private CancellationTokenSource CTS = new();

    /// <summary>
    /// This constructor demonstrates the practice of minimizing constructor implementation.
    /// </summary>
    public BestPage()
    {
        InitializeComponent();
        Loading++;
        Task.Run(LoadItems);
        Task.Run(SyncItemsLoop);
        Loading--;
    }

    /// <summary>
    /// This method demonstrates simulates downloading 12345 records from a REST service in 100 record pages.
    /// This method takes 3-6 seconds to complete, but, since it occurs in a non-UI thread users will not see the UI block.
    /// There's a 500ms delay start to minimize the impact to page navigation.
    /// </summary>
    /// <returns></returns>
    public async Task LoadItems()
    {
        Loading++;
        await Task.Delay(500);
        for (int i = 0; !CTS.IsCancellationRequested && i < 12345; i++)
        {
            lock (SampleItemsQueue)
            {
                SampleItemsQueue.Enqueue(new SampleItem());
            }
            if (i % 100 == 0)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(20));
            }
        }
        Loading--;
    }

    /// <summary>
    /// This method demonstrates a periodic update of the UI thread which takes 10ms and occurs every 200ms.
    /// Records are being transferred from the non-UI collection to the UI collection.
    /// There's a 500ms delay start to minimize the impact to page navigation.
    /// </summary>
    /// <returns></returns>
    public async Task SyncItemsLoop()
    {
        await Task.Delay(500);
        while (!CTS.IsCancellationRequested && (Loading > 0 || SampleItemsQueue.Count > 0))
        {
            if (SampleItemsQueue.Count > 0)
            {
                this.Dispatcher.Dispatch(() => SyncItems());
            }
            await Task.Delay(TimeSpan.FromMilliseconds(200));
        }
    }

    /// <summary>
    /// This method copies records from the non-UI collection to the UI collection.
    /// This method is capped to take no more than 10ms to execute.
    /// </summary>
    public void SyncItems()
    {
        Stopwatch watch = new();
        watch.Start();
        while (!CTS.IsCancellationRequested && watch.Elapsed.TotalMilliseconds < 10 && SampleItemsQueue.Count > 0)
        {
            lock (SampleItemsQueue)
            {
                SampleItems.Add(SampleItemsQueue.Dequeue());
            }
        }
    }

    /// <summary>
    /// Signal a cancellation if a page navigation occurs before the collections finished populating.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDisappearing(object sender, EventArgs e)
    {
        if (Loading > 0 && SampleItemsQueue.Count > 0)
        {
            CTS.Cancel();
        }
    }
}
