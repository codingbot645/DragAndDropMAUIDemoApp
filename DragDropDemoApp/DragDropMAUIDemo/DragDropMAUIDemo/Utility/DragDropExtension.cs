#if WINDOWS || MACCATALYST
using System;
using Microsoft.Maui.Platform;

namespace DragDropMAUIDemo;

public static class DragDropExtensions
{
	public static void RegisterDrag(this IElement element, IMauiContext? mauiContext, Func<CancellationToken, Task<Stream>> content)
	{
		Console.WriteLine("Here inside RegisterDrag");
		ArgumentNullException.ThrowIfNull(mauiContext);
		var view = element.ToPlatform(mauiContext);
        DragDropHelper.RegisterDrag(view, content);
	}

	public static void UnRegisterDrag(this IElement element, IMauiContext? mauiContext)
	{
		Console.WriteLine("Here inside UnRegisterDrag");
		ArgumentNullException.ThrowIfNull(mauiContext);
		var view = element.ToPlatform(mauiContext);
		DragDropHelper.UnRegisterDrag(view);
	}

	public static void RegisterDrop(this IElement element, IMauiContext? mauiContext, Func<Stream, Task>? content)
	{
		Console.WriteLine("Here inside RegisterDrop");
		ArgumentNullException.ThrowIfNull(mauiContext);
		var view = element.ToPlatform(mauiContext);
		DragDropHelper.RegisterDrop(view, content);
	}

	public static void UnRegisterDrop(this IElement element, IMauiContext? mauiContext)
	{
		Console.WriteLine("Here inside UnRegisterDrop");
		ArgumentNullException.ThrowIfNull(mauiContext);
		var view = element.ToPlatform(mauiContext);
		DragDropHelper.UnRegisterDrop(view);
	}
}
#endif