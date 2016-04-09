using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RaysHotDogs.Utility
{
    public class ImageHelper
    {
        public static Bitmap GetBitmapFromUrl(string url)
        {
            Bitmap bitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return bitmap;
        }

        public static Bitmap GetImageBitmapFromFilePath(string fileName, int width, int height)
        {
            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            BitmapFactory.DecodeFile(fileName, options);
            var photoWidth = options.OutWidth;
            var photoHeight = options.OutHeight;

            var scaleFactor = Math.Min(photoWidth/width, photoHeight/height);

            options.InJustDecodeBounds = false;
            options.InSampleSize = scaleFactor;
            options.InPurgeable = true;

            return BitmapFactory.DecodeFile(fileName, options);
        }
    }
}