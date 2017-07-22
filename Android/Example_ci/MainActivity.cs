using Android.App;
using Android.Widget;
using Android.OS;

using Com.Obaied.Testlibrary;
using Com.Adjust.Sdk;

namespace Example_ci
{
    [Activity(Label = "Example_ci", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private MyTestClass testKotlin;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            testKotlin = new MyTestClass();
            testKotlin.GetFoo();

            MyTestClass.GetStaticFoo();
		}
	}
}

