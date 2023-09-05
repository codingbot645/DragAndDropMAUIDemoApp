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
                ImageFrame.RegisterDrop(Handler?.MauiContext, async stream =>
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
            ImageFrame.UnRegisterDrop(Handler?.MauiContext);
        };
#endif
    }
}
