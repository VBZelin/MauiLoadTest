using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiLoadTest;

public partial class BestPage : ContentPage
{
    Queue<SampleItem> SampleItemsQueue = new();
    public ObservableCollection<SampleItem> SampleItems { get; } = new();

    private int Loading = 0;

    private CancellationTokenSource CTS = new();

    public BestPage()
    {
        InitializeComponent();
        Loading++;
        Task.Run(LoadItems);
        Task.Run(SyncItemsLoop);
        Loading--;
    }

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

    private void OnDisappearing(object sender, EventArgs e)
    {
        CTS.Cancel();
    }
}
