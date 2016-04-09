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
using RaysHotDogs.Adapters;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Fragments;

namespace RaysHotDogs
{
    [Activity(Label = "Hot Dog Menu")]
    public class HotDogMenuActivity : Activity
    {
        private readonly HotDogDataService _dataService = new HotDogDataService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HotDogMenuView);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Favourites", Resource.Drawable.FavoritesIcon, new FavouriteHotDogFragment());
            AddTab("Meat lovers", Resource.Drawable.MeatLoversIcon, new MeatLoversFragment());
            AddTab("Veggie lovers", Resource.Drawable.VeggieLoversIcon, new VeggieLoversFragment());
        }

        private void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);

                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs args)
            {
                args.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var hotDog = _dataService.GetHotDogById(data.Extras.GetInt("hotDogId"));
                var alert = new AlertDialog.Builder(this);
                alert.SetTitle("Confirmation");
                alert.SetMessage($"You've added {data.Extras.GetInt("amount")} time(s) the {hotDog.Name}");
                alert.Show();
            }
        }
    }
}