using Foundation;
using UIKit;
using System;
namespace DragDropMAUIDemo;
public static class DragDropHelper
{
    public static void RegisterDrag(UIView view, Func<CancellationToken, Task<Stream>> content)
    {

        var dragInteraction = new UIDragInteraction(new DragInteractionDelegate()
        {
            Content = content
        });
        view.AddInteraction(dragInteraction);
    }

    public static void UnRegisterDrag(UIView view)
    {
        var dragInteractions = view.Interactions.OfType<UIDragInteraction>();
        foreach (var interaction in dragInteractions)
        {
            view.RemoveInteraction(interaction);
        }
    }

    public static void RegisterDrop(UIView view, Func<Stream, Task>? content)
    {
        Console.WriteLine("register drop --------->{0}", content);
        try
        {
            var dropInteraction = new UIDropInteraction(new DropInteractionDelegate()
            {
                Content = content
            });
            view.AddInteraction(dropInteraction);
        }
        catch (Exception error)
        {
            Console.WriteLine(error);
        }
    }

    public static void UnRegisterDrop(UIView view)
    {
        Console.WriteLine("unregister drop --------->{0}", view);
        var dropInteractions = view.Interactions.OfType<UIDropInteraction>();
        foreach (var interaction in dropInteractions)
        {
            view.RemoveInteraction(interaction);
        }
    }
}

class DragInteractionDelegate : UIDragInteractionDelegate
{
    public Func<CancellationToken, Task<Stream>>? Content { get; init; }

    public override UIDragItem[] GetItemsForBeginningSession(UIDragInteraction interaction, IUIDragSession session)
    {
        if (Content is null)
        {
            return Array.Empty<UIDragItem>();
        }

        var streamContent = Content.Invoke(CancellationToken.None).GetAwaiter().GetResult();
        var itemProvider = new NSItemProvider(NSData.FromStream(streamContent), UniformTypeIdentifiers.UTTypes.Png.Identifier);
        var dragItem = new UIDragItem(itemProvider);
        return new[] { dragItem };
    }
}


class DropInteractionDelegate : UIDropInteractionDelegate
{
    public Func<Stream, Task>? Content { get; init; }

    public override UIDropProposal SessionDidUpdate(UIDropInteraction interaction, IUIDropSession session)
    {
        return new UIDropProposal(UIDropOperation.Copy);
    }

    public override void PerformDrop(UIDropInteraction interaction, IUIDropSession session)
    {
        Console.WriteLine("Here 5 ----------------->");
        var dropLocation = session.LocationInView(interaction.View);
        Console.WriteLine("perform Dropped location : " + dropLocation);

        foreach (var item in session.Items)
        {
            Console.WriteLine("Here 6 ----------------->{0}", item);
            Console.WriteLine("Here file type: ------------->{0}", item.ItemProvider.RegisteredTypeIdentifiers);
            if (item.ItemProvider.RegisteredTypeIdentifiers.Contains(UniformTypeIdentifiers.UTTypes.Json.Identifier))
            {
                Console.WriteLine("Here 6 A ----------------->{0}", item);
                item.ItemProvider.LoadItem(UniformTypeIdentifiers.UTTypes.Json.Identifier, null, async (data, error) =>
                {
                    if (data is NSUrl nsData && !string.IsNullOrEmpty(nsData.Path))
                    {
                        if (Content is not null)
                        {
                            var bytes = await File.ReadAllBytesAsync(nsData.Path);
                            await Content.Invoke(new MemoryStream(bytes));
                        }
                    }
                });
            }
            else if (item.ItemProvider.RegisteredTypeIdentifiers.Contains(UniformTypeIdentifiers.UTTypes.Jpeg.Identifier))
            {
                Console.WriteLine("Here 6 B ----------------->{0}", item);
                item.ItemProvider.LoadItem(UniformTypeIdentifiers.UTTypes.Jpeg.Identifier, null, async (data, error) =>
                {
                    if (data is NSUrl nsData && !string.IsNullOrEmpty(nsData.Path))
                    {
                        if (Content is not null)
                        {
                            var bytes = await File.ReadAllBytesAsync(nsData.Path);
                            await Content.Invoke(new MemoryStream(bytes));
                        }
                    }
                });
            }
            else if (item.ItemProvider.RegisteredTypeIdentifiers.Contains(UniformTypeIdentifiers.UTTypes.Png.Identifier))
            {
                Console.WriteLine("Here 6 C ----------------->{0}", item);
                item.ItemProvider.LoadItem(UniformTypeIdentifiers.UTTypes.Png.Identifier, null, async (data, error) =>
                {
                    if (data is NSUrl nsData && !string.IsNullOrEmpty(nsData.Path))
                    {
                        if (Content is not null)
                        {
                            var bytes = await File.ReadAllBytesAsync(nsData.Path);
                            await Content.Invoke(new MemoryStream(bytes));
                        }
                    }
                });
            }
            else if (item.ItemProvider.RegisteredTypeIdentifiers.Contains(UniformTypeIdentifiers.UTTypes.Svg.Identifier))
            {
                Console.WriteLine("Here 6 D ----------------->{0}", item);
                item.ItemProvider.LoadItem(UniformTypeIdentifiers.UTTypes.Svg.Identifier, null, async (data, error) =>
                {
                    if (data is NSUrl nsData && !string.IsNullOrEmpty(nsData.Path))
                    {
                        if (Content is not null)
                        {
                            var bytes = await File.ReadAllBytesAsync(nsData.Path);
                            await Content.Invoke(new MemoryStream(bytes));
                        }
                    }
                });
            }
            else if (item.ItemProvider.RegisteredTypeIdentifiers.Contains(UniformTypeIdentifiers.UTTypes.Heic.Identifier))
            {
                Console.WriteLine("Here 6 E ----------------->{0}", item);
                item.ItemProvider.LoadItem(UniformTypeIdentifiers.UTTypes.Heic.Identifier, null, async (data, error) =>
                {
                    if (data is NSUrl nsData && !string.IsNullOrEmpty(nsData.Path))
                    {
                        if (Content is not null)
                        {
                            var bytes = await File.ReadAllBytesAsync(nsData.Path);
                            await Content.Invoke(new MemoryStream(bytes));
                        }
                    }
                });
            }

        }
    }

}