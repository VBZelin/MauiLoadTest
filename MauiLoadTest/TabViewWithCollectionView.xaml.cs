using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiLoadTest;

public partial class MyItem : ObservableObject
{
	[ObservableProperty]
	private string _title = String.Empty;

	public MyItem()
	{
		Title = "Title " + Guid.NewGuid().ToString();
	}
}

public partial class TabViewWithCollectionView : ContentPage
{
	public ObservableCollection<MyItem> MyItems { get; } = [];

	public TabViewWithCollectionView()
	{
		InitializeComponent();

		for (int i = 0; i < 20; i++)
		{
			MyItems.Add(new MyItem());
		}
	}
}