

namespace DragDropMAUIDemo.Controls;

public partial class PhotoGridView : VerticalStackLayout
{
	public PhotoGridView()
	{
		InitializeComponent();
        collectionView.SizeChanged += OnSizeChanged;
    }


    /// <summary>
    /// Used to update size of each item in collection view
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSizeChanged(object sender, EventArgs e)
    {
        if (collectionView.ItemsSource != null)
        {
            Type itemType = collectionView.ItemsSource.GetType();
            if (itemType == typeof(ObservableCollection<ImageModel>))
            {
                ObservableCollection<ImageModel> list = (ObservableCollection<ImageModel>)collectionView.ItemsSource;
                if (collectionView.Height > 0 && collectionView.ItemsSource != null && collectionView.ItemsLayout is GridItemsLayout gridLayout)
                {
                    int numberOfColumns = gridLayout.Span;
                    double availableWidth = collectionView.Width - (numberOfColumns - 1) * 5;
                    double itemWidth = availableWidth / numberOfColumns;
                    foreach (var item in ((ObservableCollection<ImageModel>)collectionView.ItemsSource))
                    {
                        item.ImageWidth = itemWidth;
                        item.ImageHeight = itemWidth;
                    }

                }
            }
        }

    }


    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {

        Console.WriteLine("image drag started from collection view------------->");
        try
        {
            if (sender is DragGestureRecognizer gestureRecognizer)
            {
                Console.WriteLine(gestureRecognizer.BindingContext);
                if (gestureRecognizer.BindingContext is ImageModel image)
                {
                    Console.WriteLine("Dragged image: -------------->{0}", image.ImageURL);
                    e.Data.Properties.Add("ImageURL", image.ImageURL);
                }
                else
                {
                    Console.WriteLine("Dragged object  not found");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}
