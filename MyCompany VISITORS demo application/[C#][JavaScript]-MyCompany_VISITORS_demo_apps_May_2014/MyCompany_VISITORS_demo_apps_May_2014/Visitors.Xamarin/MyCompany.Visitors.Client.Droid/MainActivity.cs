using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Media.Audiofx;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MyCompany.Visitors.Client.Droid.Fragments;
using MyCompany.Visitors.Client.ViewModels;
using Android.Graphics;

namespace MyCompany.Visitors.Client.Droid
{
    [Activity(Label = "Visitors", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/Theme.Myvisitor")]
	public class MainActivity : Activity
	{
		const int ITEMS_TO_RETRIEVE_TODAY = 100;
		const int ITEMS_TO_RETRIEVE_PAGEZERO = 0;
		const int TODAY_GROUP_ID = 1;
		static MyCompanyClient client;


		public static MyCompanyClient CompanyClient
		{
			get { return client ?? (client = new MyCompanyClient(AppSettings.ApiUri.ToString() + "noauth/", "test")); }
			set { client = value; }
		}

		MainFragment mainFragment;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			AppSettings.Prefs = GetSharedPreferences("settings", FileCreationMode.Private);
			Images.Resources = Resources;

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.BaseLayout);


		    FragmentManager.BeginTransaction().Replace(Resource.Id.mainFragment, mainFragment = new MainFragment()
		    {
		        Model = new VMMainPage(),
		    }).Commit();
		    mainFragment.Model.InitializeData();
		}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_settings:
                    ShowDialog(1);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }


		protected override Dialog OnCreateDialog(int id)
		{
			if (id == 1)
			{
				var factory = LayoutInflater.From(this);
				var text_entry_view = factory.Inflate(Resource.Layout.Settings, null);

				var serverEdit = text_entry_view.FindViewById<EditText>(Resource.Id.server);
				serverEdit.Text = AppSettings.ApiUri.AbsoluteUri;

				var builder = new AlertDialog.Builder(this);
				builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
				builder.SetTitle("Settings");
				builder.SetView(text_entry_view);
				builder.SetPositiveButton("Save", (sender, args) =>
				{
					AppSettings.ApiUri = new Uri(serverEdit.Text);
                    client = new MyCompanyClient(AppSettings.ApiUri.ToString() + "noauth/", "test");
                    mainFragment.Model.InitializeData(client);
				});
				builder.SetNegativeButton("Cancel", (sender, args) =>
				{

				});

				return builder.Create();
			}
			return base.OnCreateDialog(id);
		}
	}
}

