using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using RaysHotDogs.Utility;

namespace RaysHotDogs
{
    [Activity(Label = "TakePictureActivity")]
    public class TakePictureActivity : Activity
    {
        private Button _takePictureButton;
        private ImageView _pictureView;
        private File _imageDirectory;
        private File _imageFile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TakePictureView);
            FindViews();
            HandleEvents();

            _imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "PhotosWithRay");
            if (!_imageDirectory.Exists())
                _imageDirectory.Mkdirs();
        }

        private void FindViews()
        {
            _takePictureButton = FindViewById<Button>(Resource.Id.launchCameraBtn);
            _pictureView = FindViewById<ImageView>(Resource.Id.takePictureImage);
        }

        private void HandleEvents()
        {
            _takePictureButton.Click += (sender, args) =>
            {
                var intent = new Intent(MediaStore.ActionImageCapture);
                _imageFile = new File(_imageDirectory, $"PhotoWithRay_{Guid.NewGuid()}.jpg");

                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_imageFile));

                if (intent.ResolveActivity(PackageManager) != null)
                    StartActivityForResult(intent, 0);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 0 && resultCode == Result.Ok)
            {
                var bitmap = ImageHelper.GetImageBitmapFromFilePath(_imageFile.Path, _pictureView.Width,
                    _pictureView.Height);

                _pictureView.SetImageBitmap(bitmap);
                bitmap = null;

                GC.Collect();

                AddPictureToGallery();
            }
        }

        private void AddPictureToGallery()
        {
            var intent = new Intent(Intent.ActionMediaScannerScanFile);
            intent.SetData(Android.Net.Uri.FromFile(_imageFile));
            SendBroadcast(intent);
        }
    }
}