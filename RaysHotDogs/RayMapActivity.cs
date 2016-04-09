using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RaysHotDogs
{
    [Activity(Label = "Visit Ray's store")]
    public class RayMapActivity : Activity
    {
        private FrameLayout _mapFrameLayout;
        private MapFragment _mapFragment;
        private GoogleMap _googleMap;
        private LatLng _rayLocation;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RayMapView);
            
            _rayLocation = new LatLng(50.846704, 4.352446);
            FindViews();
            CreateMapFragment();
            UpdateMapView();
        }

        private void FindViews()
        {
            _mapFrameLayout = FindViewById<FrameLayout>(Resource.Id.mapFrameLayout);
        }

        private void CreateMapFragment()
        {
            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;

            if (_mapFragment == null)
            {
                var googleMapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(true)
                    .InvokeCompassEnabled(true);

                var fragmentTransaction = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(googleMapOptions);
                fragmentTransaction.Add(Resource.Id.mapFrameLayout, _mapFragment, "map");
                fragmentTransaction.Commit();
            }
        }

        private void UpdateMapView()
        {
            var mapReadyCallBack = new LocalMapReady();

            mapReadyCallBack.MapReady += (sender, args) =>
            {
                _googleMap = (sender as LocalMapReady).Map;

                if(_googleMap == null)
                    return;

                var markerOptions = new MarkerOptions();
                markerOptions.SetPosition(_rayLocation);
                markerOptions.SetTitle("Ray's hot dogs");
                mapReadyCallBack.Map.AddMarker(markerOptions);

                var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(_rayLocation, 15);
                _googleMap.MoveCamera(cameraUpdate);
            };

            _mapFragment.GetMapAsync(mapReadyCallBack);
        }
    }
}