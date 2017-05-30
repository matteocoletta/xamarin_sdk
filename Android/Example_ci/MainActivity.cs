using Android.App;
using Android.Widget;
using Android.OS;

using Com.Adjust.Testlibrary;
using Com.Adjust.Sdk;

namespace Example_ci
{
    [Activity(Label = "Example_ci", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private CommandExecutor commandExecutor;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			string baseUrl = "https://10.0.2.2:8443";
            AdjustFactory.SetTestingMode(baseUrl);
            TestLibrary testLibrary = new TestLibrary(baseUrl, new CommandExecutor(this));
            testLibrary.InitTestSession("xamarin4.11.2@android4.11.4");
		}
    }
}

