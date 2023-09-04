using System;

namespace DragDropMAUIDemo.Model
{
	public partial class ImageModel: ObservableObject
    {

		[ObservableProperty]
		private double _imageHeight;

		[ObservableProperty]
		private double _imageWidth;

		[ObservableProperty]
		private Color _borderColor;

        public string ImageURL { get; set; }
    }
}

