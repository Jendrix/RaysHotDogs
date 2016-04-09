using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RaysHotDogs
{
    internal class LocalMapReady : Java.Lang.Object, IOnMapReadyCallback
    {
        public GoogleMap Map { get; private set; }

        public EventHandler MapReady { get; set; }

        public void OnMapReady(GoogleMap googleMap)
        {
            Map = googleMap;

            MapReady?.Invoke(this, EventArgs.Empty);
        }
    }
}