using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;

namespace RaysHotDogs.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;
        protected HotDogDataService dataService;
        protected List<HotDog> hotDogs;

        public BaseFragment()
        {
            dataService = new HotDogDataService();
        }

        protected void FindViews()
        {
            listView = View.FindViewById<ListView>(Resource.Id.hotDogListView);
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var hotDog = hotDogs[e.Position];
            var intent = new Intent(this.Activity, typeof(HotDogDetailActivity));
            intent.PutExtra("hotDogId", hotDog.HotDogId);

            StartActivityForResult(intent, 100);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var hotDog = dataService.GetHotDogById(data.Extras.GetInt("hotDogId"));
                var alert = new AlertDialog.Builder(this.Activity);
                alert.SetTitle("Confirmation");
                alert.SetMessage($"You've added {data.Extras.GetInt("amount")} time(s) the {hotDog.Name}");
                alert.Show();
            }
        }
    }
}