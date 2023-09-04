#if MACCATALYST
using Foundation;
using UIKit;
#endif

namespace DragDropMAUIDemo.Controls;

public partial class ImageStack : ContentView
{
	public ImageStack()
	{
		InitializeComponent();

#if MACCATALYST
        Loaded += (sender, args) =>
        {
            Console.WriteLine("Loaded");
            try {
                Stack.RegisterDrop(Handler?.MauiContext, async stream =>
                {
                    Console.WriteLine("GREAT ---------->{0}", stream);
                    // Read the stream and display the image
                
                });
                

            }catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        };
        Unloaded += (sender, args) =>
        {
            Stack.UnRegisterDrop(Handler?.MauiContext);
        };
#endif
    }

    void DropGestureRecognizer_Drop(System.Object sender, Microsoft.Maui.Controls.DropEventArgs e)
    {
        Console.WriteLine("inside DropGestureRecognizer_Drop --------->{0}", e);
        if (e.Data.Properties.TryGetValue("ImageURL", out object url) && url is string imageURL)
        {
            Console.WriteLine("dropped Data in DropGestureRecognizer_Drop: -------------->{0}", imageURL);


            // Add the image to your collage
            //AddImage(imageBitmap, dropPosition.X, dropPosition.Y);

            // Optionally, you can perform any required transformation of the image position
            // and update the rendering logic accordingly.


            //Point dropPosition = GetDropPosition(e);
            //Console.WriteLine("Position: --->{0}", dropPosition);
        }
    }

    private Point GetDropPosition(DropEventArgs e)
    {
        Console.WriteLine(e.Data);
        if (e.Data is IUIDropSession dropSession)
        {

            Console.WriteLine(dropSession.LocationInView);
            //Point dropPoint = dropSession.LocationInView(dropSession.LocationInView);
            return Point.Zero;
        }

        return Point.Zero;
    }
}
