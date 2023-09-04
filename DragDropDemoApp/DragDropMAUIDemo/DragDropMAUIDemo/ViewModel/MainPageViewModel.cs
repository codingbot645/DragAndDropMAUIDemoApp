using System;

namespace DragDropMAUIDemo.ViewModel
{
	public partial class MainPageViewModel:ObservableObject
	{
        #region private
        [ObservableProperty]
        private ImageModel _selectedImageModel;

        [ObservableProperty]
        private bool _multiSelect = false;

        [ObservableProperty]
        private Color _selectedBtnColor = Colors.Pink;
        #endregion

        public ObservableCollection<ImageModel> ImageList { get; } = new ObservableCollection<ImageModel>();

        public MainPageViewModel()
		{
			ImageList = new ObservableCollection<ImageModel>()
			{
				new ImageModel
				{
					ImageURL = "image_one.jpg"
				},
                new ImageModel
                {
                    ImageURL = "image_two.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_three.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_four.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_five.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_six.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_seven.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_eight.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_nine.jpg"
                },
                new ImageModel
                {
                    ImageURL = "image_ten.jpg"
                },
            };
		}

        [RelayCommand]
        void OnSelectImage(ImageModel model)
        {
            ImageModel selectedImageModel = ImageList.Where((f) => f.ImageURL == model.ImageURL).FirstOrDefault();
            int selectedIndex = -1;
            if (selectedImageModel != null)
                selectedIndex = ImageList.IndexOf(selectedImageModel);
            int targetIndex = ImageList.IndexOf(model);
            if (MultiSelect)
            {
                if ((targetIndex >= 0 || targetIndex < ImageList.Count) && selectedIndex != targetIndex)
                {
                    ImageList[targetIndex].BorderColor = ImageList[targetIndex].BorderColor == Colors.LightGray ? Colors.Blue : Colors.LightGray;
                }
            }
            else
            {
                if (model == null)
                    return;
                
                for (int i = 0; i < ImageList.Count; i++)
                {
                    if (ImageList[i].BorderColor == Colors.LightGray)
                    {
                        if (i != targetIndex)
                            continue;
                        else
                            ImageList[i].BorderColor = Colors.Blue;
                    }
                    else
                    {
                        if (i != targetIndex)
                            ImageList[i].BorderColor = Colors.LightGray;
                    }
                }
                SelectedImageModel = model;
                if (SelectedImageModel == null)
                    return;
            }
            
        }


        /// <summary>
        /// Callback for toggling the multi select status
        /// and also make items ready in collection view
        /// for selection.
        /// </summary>
        /// <param name="imageModel"></param>
        [RelayCommand]
        void SelectBtnClick(ImageModel imageModel)
        {
            MultiSelect = !MultiSelect;
            SelectedBtnColor = SelectedBtnColor == Colors.Pink ? Colors.Red : Colors.Pink;
            ImageModel targetImageModel = ImageList.Where((f) => f.ImageURL == imageModel.ImageURL).FirstOrDefault();
            int targetIndex = -1;
            if (targetImageModel != null)
                targetIndex = ImageList.IndexOf(targetImageModel);
            for (int i = 0; i < ImageList.Count; i++)
            {
                if (i != targetIndex)
                {
                    ImageList[i].BorderColor = Colors.LightGray;
                }
            }
        }
    }
}

