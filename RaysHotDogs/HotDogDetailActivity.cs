using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Utility;

namespace RaysHotDogs
{
    [Activity(Label = "HotDog Details")]
    public class HotDogDetailActivity : Activity
    {
        private ImageView _hotDogImageView;
        private TextView _hotDogNameTextView;
        private TextView _descriptionTextView;
        private TextView _priceTextView;
        private Button _cancelButton;
        private EditText _amountEditText;
        private Button _orderButton;
        private TextView _shortDescriptionTextView;

        private HotDogDataService _dataService;
        private HotDog _selectedHotDog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
            SetContentView(Resource.Layout.HotDogDetailsView);

            var hotDogId = Intent.Extras.GetInt("hotDogId");

            _dataService = new HotDogDataService();
            _selectedHotDog = _dataService.GetHotDogById(hotDogId);

            FindViews();
            BindData();
            HandleEvents();
        }

        private void FindViews()
        {
            _hotDogImageView = FindViewById<ImageView>(Resource.Id.hotDogImageView);
            _hotDogNameTextView = FindViewById<TextView>(Resource.Id.hotDogNameTextView);
            _descriptionTextView = FindViewById<TextView>(Resource.Id.descriptionTextView);
            _priceTextView = FindViewById<TextView>(Resource.Id.priceTextView);
            _cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            _amountEditText = FindViewById<EditText>(Resource.Id.amountEditText);
            _orderButton = FindViewById<Button>(Resource.Id.orderButton);
            _shortDescriptionTextView = FindViewById<TextView>(Resource.Id.shortDescriptionTextView);
        }

        private void BindData()
        {
            _hotDogNameTextView.Text = _selectedHotDog.Name;
            _descriptionTextView.Text = _selectedHotDog.Description;
            _shortDescriptionTextView.Text = _selectedHotDog.ShortDescription;
            _priceTextView.Text = "Price: " + _selectedHotDog.Price;

            var image =
                ImageHelper.GetBitmapFromUrl("http://gillcleerenpluralsight.blob.core.windows.net/files/" +
                                             _selectedHotDog.ImagePath + ".jpg");

            _hotDogImageView.SetImageBitmap(image);
        }

        private void HandleEvents()
        {
            _orderButton.Click += OrderButton_Click;
            _cancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            var amount = int.Parse(_amountEditText.Text);

            var intent = new Intent();
            intent.PutExtra("amount", amount);
            intent.PutExtra("hotDogId", _selectedHotDog.HotDogId);

            SetResult(Result.Ok, intent);

            Finish();
        }
    }
}