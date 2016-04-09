using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RaysHotDogs
{
    [Activity(Label = "RaysHotDogs", MainLauncher = true, Icon = "@drawable/icon")]
    public class MenuActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.RaysHotDogsMain);

            // Get our button from the layout resource,
            // and attach an event to it
            var menuButton = FindViewById<Button>(Resource.Id.menuBtn);
            menuButton.Click += MenuButton_Click;

            var pictureBtn = FindViewById<Button>(Resource.Id.takePictureBtn);
            pictureBtn.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(TakePictureActivity));
                StartActivity(intent);
            };

            var visitButton = FindViewById<Button>(Resource.Id.visitStoreButton);
            visitButton.Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(RayMapActivity));
                StartActivity(intent);
            };
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(HotDogMenuActivity));
            StartActivity(intent);
        }
    }
}

