using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhotoView : ContentPage
	{
		public byte[] Image { get; set; }

		public PhotoView()
		{
			InitializeComponent();
			ShowCamera();
		}

		public PhotoView(byte[] image, bool enableButtons)
		{
            InitializeComponent();
            ShowImage(image, enableButtons);
		}

		// Event fired when Shutter() is executed
        private void cameraView_MediaCaptured(object sender, Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs e)
        {
			// Get image data as bytes
			Image = e.ImageData;

			// Show image as preview to user
			ShowImage(Image, true);
			
        }

        private void TakePhoto_Clicked(object sender, EventArgs e)
		{
			// Take photo
			camera_test.Shutter();
        }

		private void ShowImage(byte[] image, bool enableButtons)
		{
			// Hide camera control
            camera_view_grid.IsVisible = false;

			// Fill ImageSource with bytes gathered from ImageData
            image_test.Source = ImageSource.FromStream(() =>
            {
                return new MemoryStream(image);
            });

			// Show image preview control
            image_view_grid.IsVisible = true;

			// Remove rows, buttons and full-size image
			if (enableButtons == false)
			{
				int childrenCount = image_view_grid.Children.Count;

                for (int i = 1; i < childrenCount; i++)
				{
					image_view_grid.Children.RemoveAt(1);
					image_view_grid.RowDefinitions.RemoveAt(1);
				}
				image_view_grid.RowDefinitions[0].Height = GridLength.Star;
			}
        }

		private void ShowCamera()
		{
			image_view_grid.IsVisible = false;
			camera_view_grid.IsVisible = true;

		}


		// Create EventHandler to react to users decision in ViewModel
		public event EventHandler<byte[]> UserActionOnPhoto;

        private void Save_Clicked(object sender, EventArgs e)
        {
			UserActionOnPhoto?.Invoke(this, Image);
            Application.Current.MainPage.Navigation.RemovePage(this);
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
			UserActionOnPhoto?.Invoke(this, null);
            Application.Current.MainPage.Navigation.RemovePage(this);
        }

        private void New_Clicked(object sender, EventArgs e)
        {
			ShowCamera();
        }

        /*
		async void CreatePhotoFile(byte[] image)
		{
			IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
			string path = Path.Combine(folder.Path, "test3.jpg");
			File.WriteAllBytes(path, image);
        }
		*/
    }
}