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
using RaysHotDogs.Utility;

namespace RaysHotDogs.Adapters
{
    public class HotDogListAdapter : BaseAdapter<HotDog>
    {
        private readonly Activity _context;
        private readonly List<HotDog> _items;

        public HotDogListAdapter(Activity context, List<HotDog> items)
        {
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var imageUrl = "http://gillcleerenpluralsight.blob.core.windows.net/files/" +
                           _items[position].ImagePath + ".jpg";

            var image = ImageHelper.GetBitmapFromUrl(imageUrl);

            if (convertView == null)
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.HotDogRowView, null);

            convertView.FindViewById<TextView>(Resource.Id.hotDogNameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.hotDogShortDescriptionTextView).Text = item.ShortDescription;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = item.Price.ToString() + "$";

            convertView.FindViewById<ImageView>(Resource.Id.hotDogImageView).SetImageBitmap(image);

            return convertView;
        }

        public override int Count => _items.Count;

        public override HotDog this[int position] => _items[position];
    }
}